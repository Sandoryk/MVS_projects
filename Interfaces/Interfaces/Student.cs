using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    class Student:IStudent
    {
        string faculty;
        int course;
        DateTime timestamp;

        public Student() { Faculty = "Metal Design"; Course = 4; timestamp = DateTime.Today; }
        public Student(int course, string fac) { Faculty = fac; Course = course; timestamp = DateTime.Today; }
        public int Course
        {
            set { course = value; }
            get { return course; }
        }
        public string Faculty
        {
            set { faculty = value; }
            get { return faculty; }
        }
        public DateTime CreationDT
        {
            get { return timestamp; }
        }
        public void FullInfoOutput()
        {
            Console.WriteLine("Faculty = {0}, Course = {1}", Faculty, Course);
            return;
        }
    }
}
