using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOrganizerDB;

namespace LibraryCatalogue.Helpers
{
    public interface ISqlDbRepository
    {
        List<Librarian> GetLibrarians();
        List<Book> GetBooks();
        List<CheckOutLog> GetCheckOutLogs();
        List<Cardholder> GetCardholders();
        List<Author> GetAuthors();
        List<Person> GetPeople();
    }
}
