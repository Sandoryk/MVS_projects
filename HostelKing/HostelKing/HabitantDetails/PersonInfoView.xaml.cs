using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for SingleHabitantView.xaml
    /// </summary>
    public partial class PersonInfoView : Window
    {
        PersonInfoViewModel oldContext;
        public PersonInfoView(PersonInfoViewModel oldViewModel)
        {
            InitializeComponent();
            this.DataContext = oldViewModel;
            this.oldContext = oldViewModel;
            oldViewModel.PropertyChanged += HabitantDetailsView_PropertyChanged;
            oldViewModel.Payments.CollectionChanged += Payments_CollectionChanged;
        }

        void Payments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
            foreach (var item in e.NewItems)
            {
                MessageBox.Show(item.GetType().FullName);
            }
        }

        void HabitantDetailsView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Object[] context = (Object[])this.DataContext;
            PersonInfoViewModel pi = (PersonInfoViewModel)context[0];
            if (pi!=null && pi.viewModelIsChanged==true)
            {
                using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    dbService.HandlePersonInfoTable(pi,t=>(t.Id==pi.Id));
                    int result = dbService.SaveChanges();
                    if (result>0)
                    {
                        this.SaveButton.IsEnabled = false;
                        this.CancelButton.IsEnabled = false;
                    }
                } 
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DataContext = this.oldContext;
            Object[] context = (Object[])this.DataContext;
            PersonInfoViewModel pi = (PersonInfoViewModel)context[0];
            pi.OnPropertyChanged("");
        }
    }
}
