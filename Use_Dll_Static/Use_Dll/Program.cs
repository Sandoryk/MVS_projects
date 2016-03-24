using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dll_SumUp;

namespace Use_Dll_Static
{
    class Program
    {
        static void Main(string[] args)
        {
            SumUP sm = new SumUP();
            Console.WriteLine("arg1: " + 42);
            Console.WriteLine("arg2: " + 35);
            Console.WriteLine("sum:  " + sm.MakeSumUp(42,35));
            Console.ReadKey();
        }
    }
}
