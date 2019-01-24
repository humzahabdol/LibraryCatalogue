using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
            foreach (var book in books)
            {
                var properties = book.GetType().GetProperties().Select(t=>t.ToString()).ToList();

                bool isFound = _searchClass.Search(keywordlist, properties);
                if (isFound)
                {
                    bookList.Add(book);
                }
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
