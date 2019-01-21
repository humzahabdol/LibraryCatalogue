using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryOrganizerDB
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() :base("LibraryContext")
        {
        }

        public DbSet<Person> People { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Cardholder> Cardholders { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CheckOutLog> CheckOutLogs { get; set; }
    }
}
