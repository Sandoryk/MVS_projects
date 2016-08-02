using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class StudentTest : IComparable<StudentTest>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TestTitle { get; set; }
        public DateTime TestDate { get; set; }
        public int TestResult { get; set; }

        public int CompareTo(StudentTest st2)
        {
            //0 - equal; 1 - st1 is greater; -1 - st2 is greater
            if (this == null)
            {
                if (st2 == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                if (st2 == null)
                {
                    return 1;
                }
                else
                {
                    return this.LastName.CompareTo(st2.LastName);
                }
            }
        }
    }
}
