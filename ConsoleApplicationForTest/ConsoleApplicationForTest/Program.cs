using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationForTest
{
    class Car
    {
        public virtual void Display()
        {
            Console.WriteLine("Car");
        }
    }
    class Car1:Car
    {
        public override void Display()
        {
            Console.WriteLine("Car1");
        }
    }
    class Car2 : Car1
    {
        public virtual void Display()
        {
            Console.WriteLine("Car2");
        }
    }
    class Car3 : Car2
    {
        public override void Display()
        {
            Console.WriteLine("Car3");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Car c = new Car3();
            c.Display();
            Console.ReadKey();
        }
    }
}
