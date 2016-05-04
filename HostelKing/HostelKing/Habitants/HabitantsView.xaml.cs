using System;
using System.Collections.Generic;
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
    public partial class HabitantsList : Window
    {
        public HabitantsList()
        {
            InitializeComponent();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs  e)
        {
            DataGridRow row = sender as DataGridRow;
            IPersonInfo inputPersonInfo = (row.Item as IPersonInfo);
            PersonInfoHabitantDetails outputPersonInfo = new PersonInfoHabitantDetails();
            PropertyInfo[] propInfos = typeof(IPersonInfo).GetProperties();
            foreach (var curPropt in propInfos)
            {
                curPropt.SetValue(outputPersonInfo, curPropt.GetValue(inputPersonInfo));
            }
            HabitantDetailsView hbDetailed = new HabitantDetailsView(outputPersonInfo);
            //hbDetailed.Owner = this;
            hbDetailed.Show();
        }
    }
}
