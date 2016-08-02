using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class StudentTestByTestTitleComparator : IComparer<StudentTest>
    {
        public int Compare(StudentTest st1, StudentTest st2)
        {
            if (st1 == null)
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
                    return st1.TestTitle.CompareTo(st2.TestTitle);
                }
            }
        }
    }
}
