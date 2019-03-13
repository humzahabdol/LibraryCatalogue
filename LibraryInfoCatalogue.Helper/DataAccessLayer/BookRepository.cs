using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryOrganizerDB;
using LibraryOrganizerDB.Entities;

namespace LibraryInfoCatalogue.Helper.DataAccessLayer
{
    public class BookRepository : IGenericSqlRepository<Book>
    {
        private readonly LibraryContext _context;
        private readonly DbSet<Book> _dbSet;

        public BookRepository()
        {
            _context = new LibraryContext();
            _dbSet = _context.Set<Book>();
        }
        public List<Book> GetAll()
        {
            return _dbSet.AsEnumerable().ToList();
        }
        public Book Find(int id)
        {
            return _dbSet.Find(id);
        }

        public Book Add(Book libEntity)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(libEntity);
            if (dbEntityEntry != null && dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            var dbset = _context.Set<Book>();
            dbset.Add(libEntity);
            _context.SaveChanges();
            return libEntity;
        }

        public void Delete(int id)
        {
            var book = _dbSet.Find(id);
            _dbSet.Remove(book);
            _context.SaveChanges();

        }

        public bool Update(Book book)
        {
            var bookInDb = _context.Books.SingleOrDefault(b => b.BookID == book.BookID);

            if (bookInDb != null)
            {
                _context.Entry(bookInDb).CurrentValues.SetValues(book);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
