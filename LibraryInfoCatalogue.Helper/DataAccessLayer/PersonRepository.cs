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
    public class PersonRepository:IGenericSqlRepository<Person>
    {
        private readonly LibraryContext _context;
        private readonly DbSet<Person> _dbSet;

        public PersonRepository()
        {
            _context = new LibraryContext();
            _dbSet = _context.Set<Person>();
        }
        public List<Person> GetAll()
        {
            return _dbSet.AsEnumerable().ToList();
        }
        public Person Find(int id)
        {
            return _dbSet.Find(id);
        }

        public Person Add(Person libEntity)
        {
            DbEntityEntry dbEntityEntry = _context.Entry(libEntity);
            if (dbEntityEntry != null && dbEntityEntry.State != EntityState.Detached)
            {
                dbEntityEntry.State = EntityState.Added;
            }
            var dbset = _context.Set<Person>();
            dbset.Add(libEntity);
            _context.SaveChanges();
            return libEntity;
        }

        public void Delete(int id)
        {
            var person = _dbSet.Find(id);
            _dbSet.Remove(person);
            _context.SaveChanges();

        }
        public bool Update(Person person)
        {
            var personInDb =
                _context.People.SingleOrDefault(a => a.PersonID == person.PersonID);
            if (personInDb != null)
            {
                _context.Entry(personInDb).CurrentValues.SetValues(person);

                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
