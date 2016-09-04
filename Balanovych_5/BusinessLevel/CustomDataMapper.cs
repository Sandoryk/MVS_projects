using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLevel
{
    static class CustomDataMapper
    {
        public static T2 DoMapping<T1, T2>(T1 inObj) 
            where T1 : class,new()
            where T2 : class, new()
        {
            T2 outObj = new T2();

            if (inObj!=null)
            {
                PropertyInfo[] propInfos = typeof(T1).GetProperties();
                foreach (var curPropt in propInfos)
                {
                    curPropt.SetValue(outObj, curPropt.GetValue(inObj));
                }
                
            }
            return outObj;
        }
    }
}
