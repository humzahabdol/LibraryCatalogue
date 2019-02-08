using LibraryInfoCatalogue.Helper.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryInfoCatalogue.Helper.BusinessClass;
using LibraryOrganizerDB;
using LibraryOrganizerDB.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace LibraryInfoCatalogue.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILibrarianHelper, LibrarianHelper>()
                .AddSingleton<IBookHelper,BookHelper>()
                .AddTransient<IGenericSqlRepository<Librarian>,LibrarianRepository>()
                .AddTransient<IGenericSqlRepository<CheckOutLog>,CheckoutLogRepository>()
                .BuildServiceProvider();

            Database.SetInitializer<LibraryContext>(new LibraryInitializer());
            


            LibrarianRepository librarianRepository = new LibrarianRepository();
            var t = librarianRepository.GetAll();

            #region Check UserName and Password is true
            var librarianHelper = serviceProvider.GetService<ILibrarianHelper>();
            var isUserLogin = librarianHelper.IsUserLogin("hoyoung", "holim1");
            #endregion

            #region Search Book
            // initialize BookHelper
            var bookHelper = serviceProvider.GetService<IBookHelper>();

            // find books search
            var z = bookHelper.FindBooksSearch("Andrew;Programming");
            var d = bookHelper.FindBooksSearch("Science Fiction");

            #endregion

            #region Check out book
            BookRepository bookRepository = new BookRepository();

            var book = bookRepository.Find(5);
            var canUserCheckout = librarianHelper.CanCheckOut(book, 16);

            CheckOutLog checkOutLog = new CheckOutLog
            {
                BookID = 5,
                CardholderID = 10,
                CheckOutDate = DateTime.Now
            };
            CheckoutLogRepository checkoutlogRepository = new CheckoutLogRepository();

            if (canUserCheckout)
            {
                var checkedOut = checkoutlogRepository.Add(checkOutLog);
            }


            var found = checkoutlogRepository.GetAll().Where(x => x.BookID == checkOutLog.BookID &&
                                                                  x.CardholderID == checkOutLog.CardholderID)
                                                        .Select(x => x).ToList();



            #endregion

            #region Update Check Out Log

            checkOutLog.BookID = 7;
            var checkOut = checkoutlogRepository.Update(checkOutLog);


            #endregion

            #region Delete Checkoutlog

            var foundCol = checkoutlogRepository.Find(8);
            checkoutlogRepository.Delete(8);
            foundCol = checkoutlogRepository.Find(8);


            #endregion


        }
    }
}
