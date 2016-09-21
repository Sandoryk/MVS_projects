using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataSourceService.Interfaces
{
    interface ITableDataGateway<T>
    {
        IEnumerable<T> GetAll();
        T GetByID(int Id);
        IEnumerable<T> GetByCondition(Func<T, bool> predicate);
        void Create(T obj);
        void Update(T obj);
        void Delete(int Id);
    }
}
