using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonInfo perInfo = new PersonInfo();
            IPerson person = perInfo;
            person.FullInfoOutput();
            IStudent student = perInfo;
            student.FullInfoOutput();
        }
    }
}
