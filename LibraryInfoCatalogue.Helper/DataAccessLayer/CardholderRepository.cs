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
    public class CardholderRepository : IGenericSqlRepository<Cardholder>
    {
        private readonly LibraryContext _context;
        private readonly DbSet<Cardholder> _dbSet;

        public CardholderRepository()
        {
            _context = new LibraryContext();
            _dbSet = _context.Set<Cardholder>();
        }
        public List<Cardholder> GetAll()
        {
            return _dbSet.AsEnumerable().ToList();
        }
        public Cardholder Find(int id)
        {
            return _dbSet.Find(id);
        }

        public Cardholder Add(Cardholder libEntity)
        {
            var dbset = _context.Set<Cardholder>();
            dbset.Add(libEntity);
            _context.SaveChanges();
            return libEntity;
        }

        public void Delete(int id)
        {
            var cardholder = _dbSet.Find(id);
            _dbSet.Remove(cardholder);
            _context.SaveChanges();

        }

        public bool Update(Cardholder cardholder)
        {
            var cardholderInDb = _context.Cardholders.SingleOrDefault(ch => ch.ID == cardholder.ID);
            if (cardholderInDb != null)
            {
                cardholderInDb = cardholder;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
