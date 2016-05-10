using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for SingleHabitantView.xaml
    /// </summary>
    public partial class PersonInfoView : Window
    {
        PersonInfoViewModel oldContext;
        public PersonInfoView(PersonInfoViewModel oldViewModel)
        {
            InitializeComponent();
            this.DataContext = oldViewModel;
            oldContext = new PersonInfoViewModel();
            PropertyInfo[] propInfos = typeof(PersonInfoViewModel).GetProperties();
            foreach (var curPropt in propInfos)
            {
                curPropt.SetValue(oldContext, curPropt.GetValue(oldViewModel));
            }
            //oldContext.Payments = new ObservableCollection<PersonPaymentsViewModel>(oldViewModel.Payments.ToList());
            oldViewModel.PropertyChanged += HabitantDetailsView_PropertyChanged;
            if (oldViewModel.Payments!=null)
            {
                oldViewModel.Payments.CollectionChanged += Payments_CollectionChanged;
                //oldContext.Payments.CollectionChanged += Payments_CollectionChanged;
            }           
        }

        void Payments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
            foreach (var item in e.NewItems)
            {
                //MessageBox.Show(item.GetType().FullName);
            }
        }

        void HabitantDetailsView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PersonInfoViewModel pi = (PersonInfoViewModel)this.DataContext;

            if (pi != null && pi.ViewModelStatus == RecordActions.Inserted)
            {
                using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    dbService.HandlePersonInfoTable(pi, null, RecordActions.Inserted);
                    int result = dbService.SaveChanges();
                    if (result > 0)
                    {
                        InitialOperations();
                    }
                } 
            }
            else if (pi != null && pi.ViewModelStatus == RecordActions.Updated)
            {
                using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    dbService.HandlePersonInfoTable(pi, t => (t.UUID == pi.UUID),RecordActions.Updated);
                    int result = dbService.SaveChanges();
                    if (result>0)
                    {
                        InitialOperations();
                    }
                } 
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (((PersonInfoViewModel)this.DataContext).ViewModelStatus==RecordActions.Inserted)
            {
                this.Close();
            }
            PersonInfoViewModel newDC = new PersonInfoViewModel();
            PropertyInfo[] propInfos = typeof(PersonInfoViewModel).GetProperties();
            foreach (var curPropt in propInfos)
            {
                curPropt.SetValue((PersonInfoViewModel)this.DataContext, curPropt.GetValue((PersonInfoViewModel)this.oldContext));
            }
            InitialOperations();

        }
        public void InitialOperations()
        {
            this.SaveButton.IsEnabled = false;
            this.CancelButton.IsEnabled = false;
            ((PersonInfoViewModel)this.DataContext).ViewModelStatus = RecordActions.NotModified;
        }
    }
}
