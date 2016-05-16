using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace HostelKing
{
    public class PersonInfoListViewModel
    {
        private List<IPersonInfo> dropList;
        public PersonInfoListViewModel(ObservableCollection<PersonInfoModel> list)
        {
            Habitants = list;
            dropList = new List<IPersonInfo>();
        }
        public event PersonChangedEventHandler OnPersonInfoChanged;
        public ObservableCollection<PersonInfoModel> Habitants { get; set; }

        /*protected void RunPersonInfoChanged(IPersonInfo pInfo)
        {
            if (OnPersonInfoChanged != null)
                PropertyChanged(this, new PersonInfoEventArgs(new PersonInfoView()));
        }*/

        public void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            PropertyInfo[] propInfos;

            DataGridRow row = sender as DataGridRow;
            PersonInfoModel outputPersonInfo = (row.Item as PersonInfoModel);
            List<PersonPaymentsModel> outputPP;
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                List<IPersonPayments> inputPP = dbService.GetPersonPaymentRecords(t => t.PersonUUID == outputPersonInfo.UUID);
                if (inputPP.Count > 0)
                {
                    outputPP = new List<PersonPaymentsModel>();
                    foreach (var item in inputPP)
                    {
                        PersonPaymentsModel newpp = new PersonPaymentsModel();
                        propInfos = typeof(IPersonPayments).GetProperties();
                        foreach (var curPropt in propInfos)
                        {
                            curPropt.SetValue(newpp, curPropt.GetValue(item));
                        }
                        outputPP.Add(newpp);
                    }
                    outputPersonInfo.Payments = new ObservableCollection<PersonPaymentsModel>(outputPP);
                }
            }
            PersonInfoView hbDetailed = new PersonInfoView(outputPersonInfo);
            hbDetailed.ParentListView = this;
            //hbDetailed.Owner = this;
            hbDetailed.Show();
        }
        public void Row_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var parent = VisualTreeHelper.GetParent((DataGridRow)sender);
            while (parent != null && parent.GetType() != typeof(DataGrid))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            var dg = parent as DataGrid;
            if (dg == null || dg.SelectedItems.Count == 0)
                return;
            dropList.Clear();
            dropList.AddRange(dg.SelectedItems.Cast<IPersonInfo>());
            var dragData = new DataObject(typeof(List<IPersonInfo>), dropList);
            DragDrop.DoDragDrop((DataGridRow)sender, dragData, DragDropEffects.Copy);
        }

        public void NewButton_Click(object sender, RoutedEventArgs e)
        {
            PersonInfoModel outputPersonInfo = new PersonInfoModel();
            outputPersonInfo.UUID = Guid.NewGuid().ToString();
            outputPersonInfo.ViewModelStatus = RecordActions.Inserted;
            PersonInfoView hbDetailed = new PersonInfoView(outputPersonInfo);
            hbDetailed.ParentListView = this;
            hbDetailed.Show();
        }
        public void SchemeButton_Click(object sender, RoutedEventArgs e)
        {
            FloorSchemeView Scheme = new FloorSchemeView();
            Scheme.Show();
        }

        //public void RefreshButton_Click(object sender, RoutedEventArgs e)
        public void RefreshButton_Click()
        {
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                List<ISettledList> tempSettledList = new List<ISettledList>();
                List<IRoom> tempRoomList = new List<IRoom>();
                foreach (var dbPer in Habitants)
                {
                    tempSettledList = dbService.GetSettledListRecords(t => t.PersonUUID == dbPer.UUID).ToList();
                    if (tempSettledList.Count > 0)
                    {
                        string roomUUID = tempSettledList[0].RoomUUID;
                        tempRoomList = dbService.GetRoomRecords(t => t.UUID == roomUUID).ToList();
                        if (tempRoomList.Count > 0)
                        {
                            dbPer.RoomNumber = tempRoomList[0].RoomNumber;
                            dbPer.SettledDate = tempSettledList[0].SettledDate;
                        }
                    }
                }
            }
        }

        public void ManagerButton_Click(object sender, RoutedEventArgs e)
        {
            RelocationManagerView relocMan = new RelocationManagerView();
            relocMan.Show();
            ((SettledListViewModel)relocMan.DataContext).OnSettledListViewModelChanged += PersonInfoListViewModel_OnSettledListViewModelChanged;
        }

        private void PersonInfoListViewModel_OnSettledListViewModelChanged(object sender, EventArgs e)
        {
            RefreshButton_Click();
        }

        public void UpdatePersonList(PersonInfoModel person, RecordActions status)
        {
            PersonInfoModel tempPerson;
            switch (status)
            {
                case RecordActions.Inserted:
                    Habitants.Add(person);
                    break;
                /*case RecordActions.Updated: //PersonInfo object itselt  is passed to DetailedWindow
                    tempPerson = Habitants.Where(t => t.UUID == person.UUID).FirstOrDefault();
                    if (tempPerson != null)
                        tempPerson = person;
                    break;*/
                case RecordActions.Deleted:
                    Habitants.Remove(person);
                    break;
            }
        }
    }
}
