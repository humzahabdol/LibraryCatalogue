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
    public class LibrarianRepository : IGenericSqlRepository<Librarian>
    {
        private readonly LibraryContext _context;
        private readonly DbSet<Librarian> _dbSet;

        public LibrarianRepository()
        {
            _context = new LibraryContext();
            _dbSet = _context.Set<Librarian>();
        }

        public List<Librarian> GetAll()
        {
            return _dbSet.AsEnumerable().ToList();
        }

        public Librarian Find(int id)
        {
            return _dbSet.Find(id);
        }

        public Librarian Add(Librarian libEntity)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(libEntity);
            if (dbEntityEntry != null && dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            var dbset = _context.Set<Librarian>();
            dbset.Add(libEntity);
            _context.SaveChanges();
            return libEntity;
        }

        public void Delete(int id)
        {
            var library = _dbSet.Find(id);
            _dbSet.Remove(library);
            _context.SaveChanges();

        }

        public bool Update(Librarian librarian)
        {
            var librarianInDb = _context.Librarians.SingleOrDefault(l => l.ID == librarian.ID);
            if (librarianInDb != null)
            {
                _context.Entry(librarianInDb).CurrentValues.SetValues(librarian);

                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
