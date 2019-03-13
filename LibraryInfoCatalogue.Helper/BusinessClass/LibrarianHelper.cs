using System;
using System.Linq;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.Helper.BusinessClass
{
    public class LibrarianHelper : ILibrarianHelper
    {
        private readonly  ExceptionHelper _exceptionHelper = new ExceptionHelper();
        private readonly IGenericSqlRepository<Librarian> _libSqlRepository = new LibrarianRepository();
        private readonly IGenericSqlRepository<CheckOutLog> _checkoutlogSqlRepository = new CheckoutLogRepository();

        public bool IsUserLogin(string userName, string password)
        {
            var librarianList = _libSqlRepository.GetAll();
            var librarian = librarianList.Where(x => x.UserID == userName && x.Password == password).Select(x=>x);
            if (librarian.Any())
            {
                return true;
            }
            foreach (var li in librarianList)
            {
                if (li.UserID == userName && li.Password == password)
                {
                    return true;
                }
            }
            
            return false;
        }

        public bool CanCheckOut(Book book,int cardholderId)
        {
            //1). A card holder with overdue books will not be allowed to check-out
            //any books until the overdue book(s) are returned;
            try
            {
                var checkOutLog = _checkoutlogSqlRepository.GetAll().Find(x => x.BookID == book.BookID &&
                                                                               x.CardholderID == cardholderId);
                if (checkOutLog != null)
                {
                    if (DateTime.Now > checkOutLog.CheckOutDate.AddDays(30)) return false;

                    //2). No book can be checked out if no copies are available.
                    //Note that several options will require you to calculate how many copies of a specific book are available.
                    var checkoutLogs = _checkoutlogSqlRepository.GetAll()
                        .Count(x => x.BookID == book.BookID && x.CardholderID == cardholderId);
                    if (checkoutLogs >= book.NumberOfCopies) return false;
                }

                return true;
            }
            catch (Exception e)
            {
                var t = _exceptionHelper.PrintAllInnerException(e);
                return false;
            }
            
        }
        
    }

    public interface ILibrarianHelper
    {
        bool IsUserLogin(string userName, string password);
        bool CanCheckOut(Book book, int cardholderId);
    }
}
