using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receipt
{
    class Program
    {
        static void Main(string[] args)
        {
            ItemWithTax it = new ItemWithTax("name", 100, 1, "20%");
            Item bit = it;
            Console.WriteLine(it.TaxGroup);
            Console.WriteLine("____");
            Console.WriteLine(bit.TaxGroup);
            Console.ReadLine();
        }
    }

}
