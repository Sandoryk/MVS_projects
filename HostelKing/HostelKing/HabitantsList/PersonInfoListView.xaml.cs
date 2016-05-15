using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HostelKing
{
    /// <summary>
    /// Interaction logic for HabitantsList.xaml
    /// </summary>
    public partial class PersonInfoListView : Window
    {
        public event MouseButtonEventHandler Redirect_HabitantsGridRow_DoubleClick;
        public event MouseButtonEventHandler Redirect_HabitantsGridRow_MouseLeftButtonDown;
        
        public PersonInfoListView()
        {
            InitializeComponent();
        }
        private void Row_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Redirect_HabitantsGridRow_MouseLeftButtonDown(sender,e);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            Redirect_HabitantsGridRow_DoubleClick(sender, e);
        }


    }
}
