using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrixHandler
{
    class Program
    {
        static void Main(string[] args)
        {
            TwoDimentionsMatrixHolder m1 = new TwoDimentionsMatrixHolder(2,3);
            m1.SetMember(0, 0, 1);
            m1.SetMember(0, 1, 2);
            m1.SetMember(0, 2, 7);
            m1.SetMember(1, 0, -8);
            m1.SetMember(1, 1, 7);
            m1.SetMember(1, 2, 9);
            TwoDimentionsMatrixHolder m2 = new TwoDimentionsMatrixHolder(2,3);
            m2.SetMember(0, 0, 7);
            m2.SetMember(0, 1, -6);
            m2.SetMember(0, 2, 2);
            m2.SetMember(1, 0, 8);
            m2.SetMember(1, 1, 65);
            m2.SetMember(1, 2, 8);
            TwoDimentionsMatrixHolder m3 = m1 + m2;
            Console.WriteLine(m3.TwoDimentionsMatrixToString());
            Console.ReadKey();
        }
    }
}
