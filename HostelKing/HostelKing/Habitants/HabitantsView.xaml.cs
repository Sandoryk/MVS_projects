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
using System.Windows.Shapes;

namespace HostelKing
{
    /// <summary>
    /// Interaction logic for HabitantsList.xaml
    /// </summary>
    public partial class HabitantsList : Window
    {
        DataBaseInteract dbService;
        public HabitantsList()
        {
            InitializeComponent();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs  e)
        {
            DataGridRow row = sender as DataGridRow;
            HabitantDetailsView hbDetailed = new HabitantDetailsView();
            hbDetailed.DataContext = (row.Item as PersonInfo);
            hbDetailed.Owner = this;
            hbDetailed.Show();
        }
    }
}
