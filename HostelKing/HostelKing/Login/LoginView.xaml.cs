using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
            this.LoginTextBox.Focus();
        }

        private void LoginWin_onKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                //FloorSchemeView fsch = new FloorSchemeView();
                //fsch.Show();
                try
                {
                     using (DataBaseConnector dbService = new DataBaseConnector())
                    {
                        dbService.db.OperationTimeOut = 5;
                        //PersonInfoListViewModel hbViewModel = new PersonInfoListViewModel(new ObservableCollection<PersonInfo>(dbService.GetHabitants()));
                        PersonInfoListView hb = new PersonInfoListView();
                        PersonInfoListViewModel hbViewModel = new PersonInfoListViewModel(new ObservableCollection<IPersonInfo>(dbService.GetAllRecords<IPersonInfo>()));
                        hb.DataContext = hbViewModel;
                        hb.Show();
                    }       
                }
                catch(SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.Close();
                }
            }
        }
    }
}
