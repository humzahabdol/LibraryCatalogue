using LibraryOrganizerDB;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace LibraryInfoCatalogue.Helper.DataAccessLayer
{
    public class AuthorRepository : IGenericSqlRepository<Author>
    {
        private readonly LibraryContext _context;
        private readonly DbSet<Author> _dbSet;

        public AuthorRepository()
        {
            _context = new LibraryContext();
            _dbSet = _context.Set<Author>();
        }
        public List<Author> GetAll()
        {
            return _dbSet.AsEnumerable().ToList();
        }
        public Author Find(int id)
        {
            return _dbSet.Find(id);
        }

        public Author Add(Author libEntity)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(libEntity);
            if (dbEntityEntry != null && dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            var dbset = _context.Set<Author>();
            dbset.Add(libEntity);
            _context.SaveChanges();
            return libEntity;
        }

        public void Delete(int id)
        {
            var author = _dbSet.Find(id);
            _dbSet.Remove(author);
            _context.SaveChanges();

        }
        public bool Update(Author author)
        {
            var authorInDb =
                _context.Authors.SingleOrDefault(a => a.ID == author.ID);
            if (authorInDb != null)
            {
                //authorInDb = author;
                _context.Entry(authorInDb).CurrentValues.SetValues(author);
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
