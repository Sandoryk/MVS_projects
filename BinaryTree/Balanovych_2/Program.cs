using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balanovych_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Program prog = new Program();

            StudentTest st1 = new StudentTest { FirstName = "Артем", LastName = "Зозуля", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 82 };
            StudentTest st2 = new StudentTest { FirstName = "Семен", LastName = "Кириллов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 71 };
            StudentTest st3 = new StudentTest { FirstName = "Игорь", LastName = "Маликов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 78 };
            StudentTest st4 = new StudentTest { FirstName = "Виталий", LastName = "Таськов", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 74 };
            StudentTest st5 = new StudentTest { FirstName = "Семен", LastName = "Доброштан", TestTitle = ".NET", TestDate = new DateTime(2016, 9, 25), TestResult = 84 };
            //BiTree<StudentTest> tree = new BiTree<StudentTest>(new StudentTestByTestTitleComparator());
            BiTree<StudentTest> tree = new BiTree<StudentTest>();
            tree.ItemRemoved += prog.Tree_ItemRemoved;
            tree.Insert(st1);
            tree.Insert(st2);
            tree.Insert(st3);
            tree.Insert(st4);
            tree.Insert(st5);
            foreach (var item in tree)
            {
                Console.WriteLine(item.LastName);
            }
            Console.WriteLine("Дерево содержит элементов: " + tree.GetTreeCount);
            Console.WriteLine("-----------------");
            tree.Remove(st2);
            Console.WriteLine("-----------------");
            foreach (var item in tree)
            {
                Console.WriteLine(item.LastName);
            }
            Console.WriteLine("Дерево содержит элементов: " + tree.GetTreeCount);

            BiTree<int> tree2 = new BiTree<int>();
            Console.WriteLine("Value type поиск-----------------");
            Console.WriteLine(tree2.Search(2));
            tree2.Insert(2);
            Console.WriteLine("Value type поиск-----------------");
            Console.WriteLine(tree2.Search(2).GetValue);
            tree2.Insert(12);
            tree2.Insert(8);
            tree2.Insert(15);
            tree2.Insert(4);
            Console.WriteLine("Value type -----------------");
            foreach (var item in tree2)
            {
                Console.WriteLine(item);
            }

            Console.ReadKey();

        }

        public void Tree_ItemRemoved(object sender, TreeItemActionEventArgs<StudentTest> e)
        {
            if (e.action == TreeAction.IsDeleted)
            {
                Console.WriteLine(((TreeNode<StudentTest>)sender).GetValue.LastName + " был удален");
            }
        }
    }
}
