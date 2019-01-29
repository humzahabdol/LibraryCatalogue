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

        public DbSet<PersonCF> People { get; set; }
        public DbSet<LibrarianCF> Librarians { get; set; }
        public DbSet<AuthorCF> Authors { get; set; }
        public DbSet<CardholderCF> Cardholders { get; set; }
        public DbSet<BookCF> Books { get; set; }
        public DbSet<CheckOutLogCF> CheckOutLogs { get; set; }
    }
}
