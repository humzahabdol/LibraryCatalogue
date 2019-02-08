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
    public class CheckoutLogRepository : IGenericSqlRepository<CheckOutLog>
    {
        private readonly LibraryContext _context;
        private readonly DbSet<CheckOutLog> _dbSet;

        public CheckoutLogRepository()
        {
            _context = new LibraryContext();
            _dbSet = _context.Set<CheckOutLog>();
        }
        public List<CheckOutLog> GetAll()
        {
            return _dbSet.AsEnumerable().ToList();
        }
        public CheckOutLog Find(int id)
        {
            return _dbSet.Find(id);
        }

        public CheckOutLog Add(CheckOutLog libEntity)
        {
            var newDbSet = _context.Set<CheckOutLog>();
            newDbSet.Add(libEntity);
            _context.SaveChanges();
            return libEntity;
        }

        public void Delete(int id)
        {
            var checkOutLog = _dbSet.Find(id);
            _dbSet.Remove(checkOutLog);
            _context.SaveChanges();
        }

        public bool Update(CheckOutLog checkOutLog)
        {
            var checkoutLoginDB =
                _context.CheckOutLogs.SingleOrDefault(cl => cl.CheckOutLogID == checkOutLog.CheckOutLogID);
            if (checkoutLoginDB != null)
            {
                checkoutLoginDB = checkOutLog;
                _context.SaveChanges();
                return true;
            }

            return false;
        }
    }
}

