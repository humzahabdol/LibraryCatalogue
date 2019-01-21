using LibraryOrganizerDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryCatalogue
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<LibraryContext>(new LibraryInitializer());
            using (LibraryContext context = new LibraryContext())
            {
                var peopleList = (from a in context.People select a).ToList();
                var librarianList = (from l in context.Librarians select l).ToList();
                var authorList = (from l in context.Authors select l).ToList();
                var cardHoldersList = (from l in context.Cardholders select l).ToList();
                var booksList = (from l in context.Books select l).ToList();
                var checkoutLogList = (from l in context.CheckOutLogs select l).ToList();

            }
        }
    }
}
