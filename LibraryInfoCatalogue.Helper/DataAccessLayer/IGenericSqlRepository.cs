using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryInfoCatalogue.Helper.DataAccessLayer
{
    public interface IGenericSqlRepository<T> where T : class
    {
        List<T> GetAll();

        T Find(int id);

        T Add(T entity);

        void Delete(int id);
        bool Update(T t); 
    }
}
