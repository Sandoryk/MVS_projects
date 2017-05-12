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
    /// Interaction logic for RelocationManagerView.xaml
    /// </summary>
    public partial class RelocationManagerView : Window
    {
        public event RoutedEventHandler Redirect_From_DataGrid_SettleButton;
        public event RoutedEventHandler Redirect_From_DataGrid_UnSettleButton;
        public RelocationManagerView()
        {
            InitializeComponent();
            SettledListViewModel stvm = new SettledListViewModel();
            RelocManDataGrid.Drop += stvm.RelocManagerDataGrid_Drop;
            CancelButton.Click += stvm.CancelButton_Click;
            Redirect_From_DataGrid_SettleButton += stvm.SettleButton_Click;
            Redirect_From_DataGrid_UnSettleButton += stvm.UnSettleButton_Click;
            this.Closing += stvm.RelocationManagerView_Closing;
            DataContext = stvm;
        }

        private void UnSettleButton_Click(object sender, RoutedEventArgs e)
        {
            Redirect_From_DataGrid_UnSettleButton(this.RelocManDataGrid,e);
        }

        private void SettleButton_Click(object sender, RoutedEventArgs e)
        {
            Redirect_From_DataGrid_SettleButton(this.RelocManDataGrid, e);
        }
    }
}
