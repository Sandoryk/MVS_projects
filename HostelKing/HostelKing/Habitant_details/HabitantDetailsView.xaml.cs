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
    /// Interaction logic for SingleHabitantView.xaml
    /// </summary>
    public partial class HabitantDetailsView : Window
    {
        PersonInfoHabitantDetails oldViewModel;
        public HabitantDetailsView(PersonInfoHabitantDetails oldViewModel)
        {
            InitializeComponent();
            this.DataContext = oldViewModel;
            this.oldViewModel = oldViewModel;
            this.oldViewModel.PropertyChanged += HabitantDetailsView_PropertyChanged;
        }

        void HabitantDetailsView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PersonInfoHabitantDetails pi = (PersonInfoHabitantDetails)this.DataContext;
            if (pi!=null)
            {
                using (DataBaseInteract dbService = new DataBaseInteract())
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
    }
}
