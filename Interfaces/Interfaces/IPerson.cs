using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IPerson
    {
        string Name { get; set; }
        int Age { get; set; }
        DateTime DateOfBirth { get; set; }
        DateTime CreationDT { get; }
        /*void GetName();
        void GetAge();
        void GetDateOfBirth();*/

        void FullInfoOutput();

    }
}
