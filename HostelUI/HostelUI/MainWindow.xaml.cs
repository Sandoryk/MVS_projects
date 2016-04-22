using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HostelUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (HostelDBContext db = new HostelDBContext())
            {
                // создаем два объекта User
                PersonInfo user1 = new PersonInfo { FirstName = "Tom", LastName = "Fisko", DateBirth = DateTime.Now };
                PersonInfo user2 = new PersonInfo { FirstName = "Sam", LastName = "Howoer", DateBirth = DateTime.Now };

                // добавляем их в бд
                db.PersonInfoList.Add(user1);
                db.PersonInfoList.Add(user2);
                db.SaveChanges();
                Console.WriteLine("Объекты успешно сохранены");

                // получаем объекты из бд и выводим на консоль
                var users = db.PersonInfoList;
                Console.WriteLine("Список объектов:");
                foreach (PersonInfo u in users)
                {
                    Console.WriteLine("{0}.{1} - {2}", u.Id, u.FirstName, u.LastName);
                }
                Console.Read();
            }
        }
    }
}
