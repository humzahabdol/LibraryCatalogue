using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOrganizerDB;

namespace LibraryCatalogue.Helpers
{
    public class SqlDbRepository : ISqlDbRepository
    {
        protected readonly LibraryContext _context;
        public SqlDbRepository()
        {
            _context = new LibraryContext();
        }
        public List<Librarian> GetLibrarians()
        {
            return _context.Librarians.ToList();
        }

        public List<Person> GetPeople()
        {
            return _context.People.ToList();
        }

        public List<Author> GetAuthors()
        {
            return _context.Authors.ToList();
        }

        public List<Cardholder> GetCardholders()
        {
            return _context.Cardholders.ToList();
        }

        public List<CheckOutLog> GetCheckOutLogs()
        {
            return _context.CheckOutLogs.ToList();
        }

        public List<Book> GetBooks()
        {
            return _context.Books.ToList();
        }
    }
}
