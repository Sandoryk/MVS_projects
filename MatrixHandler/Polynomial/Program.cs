using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polynomial
{
    class Program
    {
        static void Main(string[] args)
        {
            PolinomHolder ph = new PolinomHolder();
            ph.AddMemberWithDegree(0, 2.5);
            ph.AddMemberWithDegree(2, 21);
            ph.AddMemberWithDegree(-1, 35);
            ph.AddMemberWithDegree(1, -71);
            PolinomHolder ph2 = new PolinomHolder();
            Console.WriteLine(ph.PolinomToString());
            ph2.AddMemberWithDegree(2,-30);
            ph2.AddMemberWithDegree(1, 12);
            Console.WriteLine(ph2.PolinomToString());
            PolinomHolder phResult = ph - ph2;
            Console.WriteLine(phResult.PolinomToString());
            Console.ReadKey();
        }
    }
}
