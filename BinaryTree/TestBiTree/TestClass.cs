using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Balanovych_2;
using System.IO;

namespace TestBiTree
{
    [TestClass]
    public class TestClass
    {
        [TestMethod]
        public void If_InsertMethod_Increases_BiTree_Count()
        {
            StudentTest st1 = new StudentTest { FirstName = "Артем", LastName = "Зозуля", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 82 };
            BiTree<StudentTest> tree = new BiTree<StudentTest>();
            int expectedCount = 1;

            tree.Insert(st1);

            Assert.AreEqual(expectedCount, tree.GetTreeCount);
        }

        [TestMethod]
        public void If_Five_Elements_Inserted_Into_BiTree_In_Alphabetic_Order()
        {
            StudentTest st1 = new StudentTest { FirstName = "Артем", LastName = "Зозуля", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 82 };
            StudentTest st2 = new StudentTest { FirstName = "Семен", LastName = "Кириллов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 71 };
            StudentTest st3 = new StudentTest { FirstName = "Игорь", LastName = "Маликов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 78 };
            StudentTest st4 = new StudentTest { FirstName = "Виталий", LastName = "Таськов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 74 };
            StudentTest st5 = new StudentTest { FirstName = "Семен", LastName = "Доброштан", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 84 };
            BiTree<StudentTest> tree = new BiTree<StudentTest>();
            StudentTest[] studentsArray = new StudentTest[5];
            int count = 0;

            tree.Insert(st1);
            tree.Insert(st2);
            tree.Insert(st3);
            tree.Insert(st4);
            tree.Insert(st5);
            foreach (var student in tree)
            {
                studentsArray[count] = student;
                count++;
            }

            for (int i = 0; i < studentsArray.Length-1; i++)
            {
                Assert.IsTrue((studentsArray[i+1].CompareTo(studentsArray[i])>=0),"Elements not in alphabetic order. Method result: " + studentsArray[i + 1].LastName + " >= " + studentsArray[i].LastName);
            }
        }

        [TestMethod]
        public void If_RemoveMethod_Decreases_BiTree_Count()
        {
            StudentTest st1 = new StudentTest { FirstName = "Артем", LastName = "Зозуля", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 82 };
            BiTree<StudentTest> tree = new BiTree<StudentTest>();
            int expectedCount = 0;

            tree.Insert(st1);
            tree.Remove(st1);

            Assert.AreEqual(expectedCount, tree.GetTreeCount);
        }

        [TestMethod]
        public void If_SearchMethod_Searches_Correct_Node()
        {
            StudentTest st1 = new StudentTest { FirstName = "Артем", LastName = "Зозуля", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 82 };
            StudentTest st2 = new StudentTest { FirstName = "Семен", LastName = "Кириллов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 71 };
            StudentTest st3 = new StudentTest { FirstName = "Игорь", LastName = "Маликов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 78 };
            StudentTest st4 = new StudentTest { FirstName = "Виталий", LastName = "Таськов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 74 };
            StudentTest st5 = new StudentTest { FirstName = "Семен", LastName = "Доброштан", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 84 };
            BiTree<StudentTest> tree = new BiTree<StudentTest>();

            tree.Insert(st1);
            tree.Insert(st2);
            tree.Insert(st3);
            tree.Insert(st4);
            tree.Insert(st5);
            TreeNode<StudentTest> node = tree.Search(st3);
            StudentTest expectedSt = node.GetValue;

            Assert.IsTrue(expectedSt.CompareTo(st3) == 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException),"No elements in BiTree")]
        public void Nothing_To_Remove_In_BeTree_If_Try_To_Remove()
        {
            StudentTest st1 = new StudentTest { FirstName = "Артем", LastName = "Зозуля", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 82 };
            BiTree<StudentTest> tree = new BiTree<StudentTest>();

            tree.Remove(st1);
        }
    }
}
