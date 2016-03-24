using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Use_Dll_Dynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            string parentpath = "";
            bool fileexistsf = false;

            DirectoryInfo dir = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location);
            while(dir != null)
            {
                if (File.Exists(dir.FullName + @"\Dll_SumUp.dll"))
                {
                    fileexistsf = true;
                    parentpath = dir.FullName;
                    break;
                }
                else
                    dir = Directory.GetParent(dir.FullName);
            }
            if (fileexistsf)
            {
                Assembly sumdll = Assembly.LoadFrom(parentpath + @"\Dll_SumUp.dll");
                Type type = sumdll.GetType("Dll_SumUp.SumUP");
                if (type != null)
                {
                    MethodInfo methodInfo = type.GetMethod("MakeSumUp");
                    if (methodInfo != null)
                    {
                        object result;
                        ParameterInfo[] parameters = methodInfo.GetParameters();
                        object classInstance = Activator.CreateInstance(type, null);
                        if (parameters.Length != 0)
                        {
                            object[] parametersArray = new object[] { 12,58 };
                            Console.WriteLine("arg1: " + parametersArray[0]);
                            Console.WriteLine("arg2: " + parametersArray[1]);
                            result = methodInfo.Invoke(classInstance, parametersArray);
                            Console.WriteLine("sum:  " + result);
                            Console.ReadKey();
                        }
                    }
                }
            }
        }
    }
}
