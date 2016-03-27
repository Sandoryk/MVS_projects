using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Interfaces
{
    class PersonInfo : IPerson, IStudent
    {
        string name,faculty;
        int age,course;
        DateTime date,timestamp;
        public PersonInfo()
        {
            DateOfBirth = DateTime.ParseExact("1992-03-21 13:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
            Age = 24;
            Faculty = "Metal Design"; 
            Course = 4; 
            timestamp = DateTime.Today;
        }
        public string Name
        {
            set { name=value; }
            get { return name; }
        }
        public int Age
        {
            set { age = value; }
            get { return age; }
        }
        public DateTime DateOfBirth
        {
            set { date = value; }
            get { return date; }
        }
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
        DateTime IStudent.CreationDT
        {
            get { return timestamp; }
        }
        DateTime IPerson.CreationDT
        {
            get { return timestamp; }
        }
        void IPerson.FullInfoOutput() 
        {
            Console.WriteLine("Name = {0}, Age = {1}, Date = {2}", Name, Age, DateOfBirth);
            return; 
        }
        void IStudent.FullInfoOutput()
        {
            Console.WriteLine("Faculty = {0}, Course = {1}", Faculty, Course);
            return;
        }
    }
}
