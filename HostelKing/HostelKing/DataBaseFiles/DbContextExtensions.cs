using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public  static class DbContextExtensions
    {
        public static IEnumerable<T> SetOf<T>(this DbContext dbContext) where T : class
        {
            return dbContext.GetType().Assembly.GetTypes()
                .Where(type => typeof(T).IsAssignableFrom(type) && !type.IsInterface && type.GetInterface(typeof(IDataBaseModel).ToString())!=null)
                .SelectMany(t => Enumerable.Cast<T>(dbContext.Set(t)));
        }
    }
}
