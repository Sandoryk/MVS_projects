using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public interface ITableDataGateway<T>
    {
        List<T> FindAll();
        T FindByID(int ID);
        List<T> FindByCondition(Func<T, bool> predicate);
        Boolean Insert(T obj);
        Boolean Update(T obj);
        Boolean Delete(T obj);
    }
}
