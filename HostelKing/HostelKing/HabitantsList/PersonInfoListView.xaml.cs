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
        public PersonInfoListView()
        {
            InitializeComponent();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs  e)
        {
            PropertyInfo[] propInfos;

            DataGridRow row = sender as DataGridRow;
            IPersonInfo inputPersonInfo = (row.Item as IPersonInfo);
            PersonInfoViewModel outputPersonInfo = new PersonInfoViewModel();
            propInfos = typeof(IPersonInfo).GetProperties();
            foreach (var curPropt in propInfos)
            {
                curPropt.SetValue(outputPersonInfo, curPropt.GetValue(inputPersonInfo));
            }

            List<PersonPaymentsViewModel> outputPP = new List<PersonPaymentsViewModel>();
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                List<IPersonPayments> inputPP = dbService.GetPersonPaymentsRecords(t=>t.PersonId==outputPersonInfo.Id);
                if (inputPP.Count>0)
                {
                    foreach (var item in inputPP)
                    {
                        PersonPaymentsViewModel newpp = new PersonPaymentsViewModel();
                         propInfos = typeof(IPersonPayments).GetProperties();
                         foreach (var curPropt in propInfos)
                         {
                             curPropt.SetValue(newpp, curPropt.GetValue(item));
                         }
                         outputPP.Add(newpp);
                    }
                    outputPersonInfo.Payments = new ObservableCollection<PersonPaymentsViewModel>(outputPP);
                }
            }
            PersonInfoView hbDetailed = new PersonInfoView(outputPersonInfo);
            //hbDetailed.Owner = this;
            hbDetailed.Show();
        }
    }
}
