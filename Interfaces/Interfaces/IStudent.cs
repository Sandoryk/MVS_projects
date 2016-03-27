using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    interface IStudent
    {
        int Course { get; set; }
        string Faculty { get; set; }
        DateTime CreationDT { get; }
        /*void GetCourse();
        void GetFaculty();*/
        void FullInfoOutput();
    }
}
