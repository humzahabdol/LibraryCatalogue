using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryInfoCatalogue.Helper.DataAccessLayer;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.Helper.BusinessClass
{
    public class BookHelper : IBookHelper
    {
        private readonly BookRepository _bookSqlRepository = new BookRepository();
        private readonly CheckoutLogRepository _checkOutLogSqlRepository = new CheckoutLogRepository();
        private readonly CardholderRepository _cardholderRepository= new CardholderRepository();
 
        public List<Book> FindBooksSearch(string keywordsText)
        {
            var keywords = SplitKeywords(keywordsText);
            List<Book> booklist = new List<Book>();
            foreach (var keyword in keywords)
            {
                var lowerKeyword = keyword.ToLower();
                var books = _bookSqlRepository.GetAll().Where(x => x.Title?.ToLower().Replace(" ","") == lowerKeyword ||
                                                                   x.Author.Person.FirstName.ToLower() == lowerKeyword ||
                                                                   x.Author.Person.LastName.ToLower() == lowerKeyword ||
                                                                   x.Subject?.ToLower().Replace(" ","") == lowerKeyword ||
                                                                   x.ISBN.ToLower() == lowerKeyword).Select(x => x).ToList();
                booklist = books;
            }

            return booklist;
        }

        public int CountAvailable(Book book)
        {
            var checkout = _checkOutLogSqlRepository.GetAll().Count(x => x.BookID == book.BookID);
            return book.NumberOfCopies - checkout;
        }
        public bool CheckOutBook(CheckOutLog checkOutLog)
        {
            var checkedOut = _checkOutLogSqlRepository.Add(checkOutLog);

            if (checkedOut != null)
            {
                return true;
            }
            return false;
        }

        public string CanCheckOutBook(Book book)
        {
            var checkoutCount = _checkOutLogSqlRepository.GetAll().Count(x => x.BookID == book.BookID);
            if (book.NumberOfCopies > checkoutCount)
            {
                return "Yes";
            }

            return "No";
        }

        public List<string> SplitKeywords(string s)
        {
            char[] delimiters = { ';' };
            string[] words = s.Split(delimiters);
            List<string> keywords = new List<string>(words.Length);
            foreach (string word in words)
            {
                if (word != string.Empty)
                {
                    keywords.Add(word.Replace(" ", ""));
                }
            }
            return keywords;
        }
    }

    public interface IBookHelper
    {
        List<Book> FindBooksSearch(string keywordsText);
        bool CheckOutBook(CheckOutLog checkOutLog);
        string CanCheckOutBook(Book book);
        int CountAvailable(Book book);
        List<string> SplitKeywords(string s);
    }
}
