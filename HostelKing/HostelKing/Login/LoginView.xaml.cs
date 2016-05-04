using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginWin_onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //HabitantsList hb = new HabitantsList();
                FloorSchemeView fsch = new FloorSchemeView();
                fsch.Show();
                this.Close();
                /*using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    //HabitantsViewModel hbViewModel = new HabitantsViewModel(new ObservableCollection<PersonInfo>(dbService.GetHabitants()));
                    HabitantsViewModel hbViewModel = new HabitantsViewModel(dbService.GetAllRecords<IPersonInfo>());
                    hb.DataContext = hbViewModel;
                    //hb.HabitantsGrid.ItemsSource = dbService.GetHabitants();
                }       
                hb.Show();*/
            }
        }
    }
}
