using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOrganizerDB.Entities;

namespace LibraryOrganizerDB
{
    public class LibraryContext : DbContext
    {
        public LibraryContext() : base("LibraryContext")
        {
        }
        public LibraryContext(bool enableLazyLoading, bool enableProxyCreation)
            : base("Name=LibraryContext")
        {
            Configuration.ProxyCreationEnabled = enableProxyCreation;
            Configuration.LazyLoadingEnabled = enableLazyLoading;
        }
        public LibraryContext(DbConnection connection)
            : base(connection, true)
        {
            Configuration.LazyLoadingEnabled = false;
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<LibraryContext>());
        }

        public IDbSet<Person> People { get; set; }
        public IDbSet<Librarian> Librarians { get; set; }
        public IDbSet<Author> Authors { get; set; }
        public IDbSet<Cardholder> Cardholders { get; set; }
        public IDbSet<Book> Books { get; set; }
        public IDbSet<CheckOutLog> CheckOutLogs { get; set; }

    }
}
