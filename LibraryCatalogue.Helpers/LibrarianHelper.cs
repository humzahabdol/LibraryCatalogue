using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LibraryCatalogue.Entities;
using LibraryOrganizerDB;
using Search;

namespace LibraryCatalogue.Helpers
{
    
    public class LibrarianHelper : ILibrarianHelper
    {
        private readonly ISqlDbRepository _sqlDbRepository;
        private readonly ISearchClass _searchClass; 

        public LibrarianHelper(ISqlDbRepository sqlDbRepository, ISearchClass searchClass)
        {
            _searchClass = searchClass;
            _sqlDbRepository = sqlDbRepository;
        }

        public bool IsUserLogin(string userName, string password)
        {
            var librarian = _sqlDbRepository.GetLibrarians().Where(l => l.UserID == userName && l.Password == password);
            if (librarian.Any())
            {
                return true;
            }

            return false;
        }

        public List<Book> FilterBooks(string keywords)
        {
            var keywordlist = _searchClass.SplitKeywords(keywords);
            var books = _sqlDbRepository.GetBooks();
            List<Book> bookList = new List<Book>();
            List<string> properties = new List<string>();

            foreach (var book in books)
            {
                properties.Add(book.Title);
                properties.Add(book.FirstName);
                properties.Add(book.LastName);
                properties.Add(book.ISBN);
                if (book.Subject != null)
                {
                    properties.Add(book.Subject);
                }
                bool isFound = _searchClass.Search(keywordlist, properties);
                if (isFound)
                {
                    bookList.Add(book);
                }
                properties.Clear();
            }

            return bookList;
        }

    }

    public interface ILibrarianHelper
    {
        bool IsUserLogin(string userName, string password);
        List<Book> FilterBooks(string keywords);
    }
}
