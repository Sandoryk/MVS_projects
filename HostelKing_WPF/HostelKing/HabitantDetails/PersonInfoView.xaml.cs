using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
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
    public delegate void PersonChangedEventHandler(object sender, PersonInfoEventArgs e);
    public partial class PersonInfoView : Window
    {
        PersonInfoModel oldContext;
        MemoryStream ms;
        BinaryFormatter oldContextKeeper;
        List<PersonPaymentsModel> removedPayments;
        
        public PersonInfoView(PersonInfoModel oldViewModel)
        {
            InitializeComponent();
            removedPayments = new List<PersonPaymentsModel>();
            this.DataContext = oldViewModel;
            oldContextKeeper = new BinaryFormatter();
            ms = new MemoryStream();
            oldContextKeeper.Serialize(ms, oldViewModel);
            MakeAttachmentsToEvents(oldViewModel);
       
        }
        public bool FieldsAreEditable { get; set; }
        public PersonInfoListViewModel ParentListView { get; set; }
        public void MakeAttachmentsToEvents(PersonInfoModel pInfo)
        {
            pInfo.PropertyChanged += HabitantDetailsView_PropertyChanged;
            if (pInfo.Payments != null)
            {
                pInfo.Payments.CollectionChanged += Payments_CollectionChanged;
            } 
        }
        private void DataGridBeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
        }
        void HabitantDetailsView_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
        }
        void Payments_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            this.SaveButton.IsEnabled = true;
            this.CancelButton.IsEnabled = true;
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Remove)
            {
                IList removed = e.OldItems;
                foreach (var item in removed)
                {
                    removedPayments.Add((PersonPaymentsModel)item);
                }
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            PersonInfoModel pInfo = (PersonInfoModel)this.DataContext;

            if (String.IsNullOrEmpty(pInfo.FirstName))
            {
                MessageBox.Show("Укажите имя");
                return;
            }
            if (String.IsNullOrEmpty(pInfo.LastName))
            {
                MessageBox.Show("Укажите фамилию");
                return;
            }
            if (String.IsNullOrEmpty(pInfo.Sex))
            {
                MessageBox.Show("Укажите пол");
                return;
            }
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                bool savef = false;
                if (pInfo != null && pInfo.ViewModelStatus == RecordActions.Inserted)
                {
                    pInfo.UUID = Guid.NewGuid().ToString();
                    dbService.HandlePersonInfoTable(pInfo, null, RecordActions.Inserted);
                    savef = true;
                }
                else if (pInfo != null && pInfo.ViewModelStatus == RecordActions.Updated)
                {
                    dbService.HandlePersonInfoTable(pInfo, t => (t.UUID == pInfo.UUID), RecordActions.Updated);
                    savef = true;
                }
                if (pInfo.Payments != null && pInfo.Payments.Count > 0)
                {

                    foreach (var payment in pInfo.Payments)
                    {
                        if (String.IsNullOrEmpty(payment.PersonUUID) == true)
                        {
                            payment.PersonUUID = pInfo.UUID;
                            payment.UUID = Guid.NewGuid().ToString();
                            payment.ViewModelStatus = RecordActions.Inserted;
                        }
                        if (payment.ViewModelStatus == RecordActions.Inserted)
                            dbService.HandlePersonPaymentsTable(payment, null, RecordActions.Inserted);
                        else if (payment.ViewModelStatus == RecordActions.Updated)
                            dbService.HandlePersonPaymentsTable(payment, t => (t.UUID == payment.UUID), RecordActions.Updated);
                    }
                    if (removedPayments.Count>0)
                    {
                        foreach (var payment in removedPayments)
                        {
                            if (String.IsNullOrEmpty(payment.UUID) == false)
                            {
                                dbService.HandlePersonPaymentsTable(payment, t => (t.UUID == payment.UUID), RecordActions.Deleted);
                            }
                        }
                        removedPayments = new List<PersonPaymentsModel>();
                    }
                    savef = true;
                }
                if (savef==true)
                {
                    int result = dbService.SaveChanges();
                    if (result > 0)
                    {
                        if (ParentListView != null)
                            ParentListView.UpdatePersonList(pInfo, pInfo.ViewModelStatus);
                        InitialOperations();
                        ms.Position = 0;
                        oldContextKeeper.Serialize(ms, pInfo); 
                    }
                }
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            PersonInfoModel pInfo = (PersonInfoModel)this.DataContext;

            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                dbService.HandlePersonInfoTable(pInfo, t => (t.UUID == pInfo.UUID), RecordActions.Deleted);
                if (pInfo.Payments != null && pInfo.Payments.Count > 0)
                {
                    foreach (var payment in pInfo.Payments)
                    {
                        dbService.HandlePersonPaymentsTable(payment, t => (t.UUID == payment.UUID), RecordActions.Deleted);
                    }
                    if (removedPayments.Count > 0)
                    {
                        foreach (var payment in removedPayments)
                        {
                            dbService.HandlePersonPaymentsTable(payment, t => (t.UUID == payment.UUID), RecordActions.Deleted);
                        }
                        removedPayments = new List<PersonPaymentsModel>();
                    }
                }
                int result = dbService.SaveChanges();
                if (result > 0)
                {
                    if (ParentListView!=null)
                        ParentListView.UpdatePersonList(pInfo, RecordActions.Deleted);
                    this.Close();
                }
            }
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (((PersonInfoModel)this.DataContext).ViewModelStatus==RecordActions.Inserted)
            {
                this.Close();
            }
            else
            {
                ms.Position = 0;
                oldContext = (PersonInfoModel)oldContextKeeper.Deserialize(ms);
                MakeAttachmentsToEvents(oldContext);
                this.DataContext = oldContext;
                removedPayments = new List<PersonPaymentsModel>();
                InitialOperations();
            }
        }
        public void InitialOperations()
        {
            this.SaveButton.IsEnabled = false;
            this.CancelButton.IsEnabled = false;
            ((PersonInfoModel)this.DataContext).ViewModelStatus = RecordActions.NotModified;
        }

        private void HabitantDetailsWin_Closing(object sender, CancelEventArgs e)
        {
            ms.Close();
        }

    }
}
