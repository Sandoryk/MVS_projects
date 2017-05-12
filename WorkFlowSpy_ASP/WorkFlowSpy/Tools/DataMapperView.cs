using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WorkFlowSpy
{
    public static class DataMapperView
    {
        public static T2 DoMapping<T1, T2>(T1 inObj)
           where T1 : class, new()
           where T2 : class, new()
        {
            T2 outObj = new T2();

            if (inObj != null)
            {
                PropertyInfo[] outPropInfo = typeof(T1).GetProperties();
                foreach (var outPropt in outPropInfo)
                {
                    PropertyInfo inProp = typeof(T2).GetProperty(outPropt.Name);
                    if (inProp != null)
                    {
                        inProp.SetValue(outObj, outPropt.GetValue(inObj));
                    }
                }
            }
            return outObj;
        }
    }
}