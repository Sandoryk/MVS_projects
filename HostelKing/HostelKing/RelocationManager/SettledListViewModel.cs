using System;
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
using System.Windows.Media;

namespace HostelKing
{
    public class SettledListViewModel
    {
        ObservableCollection<SettledListModel> settledAllList;
        MemoryStream ms;
        BinaryFormatter oldSettleAllListKeeper;
        
        public SettledListViewModel()
        {
            RoomList = new List<IRoom>();
            ManagerList = new ObservableCollection<SettledListModel>();
            GetAllRooms();
        }
        public List<IRoom> RoomList { get; set; }
        public ObservableCollection<SettledListModel> ManagerList { get; set; }
        public void GetAllRooms()
        {
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                if (RoomList != null)
                {
                    RoomList = dbService.GetAllRecords<IRoom>();
                }
            }
        }
        public void InsertRowIntoManager(List<IPersonInfo> pInfoList)
        {
            SettledListModel settle;
            PropertyInfo[] propInfos;

            if (pInfoList != null)
            {
                using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    propInfos = typeof(ISettledList).GetProperties();
                    foreach (var pInfo in pInfoList)
                    {
                        if (settledAllList==null)
                        {
                            settledAllList = new ObservableCollection<SettledListModel>();
                            List<ISettledList>  tempSettleList = dbService.GetAllRecords<ISettledList>();
                            foreach (var setUnit in tempSettleList)
                            {
                                settle = new SettledListModel();
                                foreach (var curPropt in propInfos)
                                {
                                    curPropt.SetValue(settle, curPropt.GetValue(setUnit));
                                }
                                /*IPersonInfo pi = dbService.GetPersonRecords(t=>t.UUID==settle.PersonUUID).First();
                                if (pi!=null)
                                {
                                    settle.FirstName = pi.FirstName;
                                    settle.LastName = pi.LastName;
                                    settle.Sex = pi.Sex;
                                }*/
                                IRoom room = dbService.GetRoomRecords(t => t.UUID == settle.RoomUUID).First();
                                if (room!=null)
                                {
                                    settle.RoomNumber = room.RoomNumber;
                                }
                                settledAllList.Add(settle);
                            }
                            if (oldSettleAllListKeeper == null)
                                oldSettleAllListKeeper = new BinaryFormatter();
                            if (ms == null)
                                ms = new MemoryStream();
                            else
                                ms.Position = 0;
                            oldSettleAllListKeeper.Serialize(ms, settledAllList);
                        }
                        List<SettledListModel> curSettleList = settledAllList.Where(t => t.PersonUUID == pInfo.UUID).ToList<SettledListModel>();
                        if (curSettleList.Count > 1)
                        {
                            MessageBox.Show("Невозможно добавить строку в менеджер поселения");
                        }
                        else
                        {
                            
                            if (curSettleList.Count == 1)
                            {
                                settle = curSettleList[0];
                                settle.FirstName = pInfo.FirstName;
                                settle.LastName = pInfo.LastName;
                                settle.Sex = pInfo.Sex;
                            }
                            else
                            {
                                settle = new SettledListModel();
                                settle.UUID = Guid.NewGuid().ToString();
                                settle.PersonUUID = pInfo.UUID;
                                settle.FirstName = pInfo.FirstName;
                                settle.LastName = pInfo.LastName;
                                settle.Sex = pInfo.Sex;
                            }
                            ManagerList.Add(settle);
                        }  
                    }
                }
            }
        }

        public void RelocManagerDataGrid_Drop(object sender, DragEventArgs e)
        {
            var dataObj = e.Data as DataObject;
            var dropped = dataObj.GetData(typeof(List<IPersonInfo>)) as List<IPersonInfo>;
            InsertRowIntoManager(dropped);
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
        public void MakeDataGridRowFocused(DataGrid dg, int index)
        {
            dg.SelectedItem = dg.Items[index];
            dg.ScrollIntoView(dg.Items[index]);
        }

        public void SettleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(DataGrid))
            {
                MessageBox.Show("Невозможно определить список");
                return;
            }
            var dg = sender as DataGrid;
            if (dg == null)
            {
                MessageBox.Show("Невозможно определить список");
                return;
            }
            else
            {
                var toSettleList = dg.Items.OfType<SettledListModel>();
                int count = 0;

                using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    ObservableCollection<SettledListModel> settledAllListForChecks = new ObservableCollection<SettledListModel>();
                    if (ms!=null)
                    {
                        ms.Position = 0;
                        settledAllListForChecks = (ObservableCollection<SettledListModel>)oldSettleAllListKeeper.Deserialize(ms);
                    }
                    bool savef = false;
                    foreach (var row in toSettleList)
                    {
                        if (String.IsNullOrEmpty(row.RoomNumber))
                        {
                            MessageBox.Show("Укажите комнату");
                            MakeDataGridRowFocused(dg, count);
                            return;
                        }

                        string newRoomUUID = "";
                        try
                        {
                            newRoomUUID = RoomList.Where(t => t.RoomNumber == row.RoomNumber).First().UUID;
                        }
                        catch(NullReferenceException nullEx)
                        {
                            MessageBox.Show("Такого номера команты нет в базе данных");
                            return;
                        }
                    
                        if (String.IsNullOrEmpty(row.RoomUUID)==false && row.RoomUUID != newRoomUUID)
                            row.ViewModelStatus = RecordActions.Updated;
                        else if (String.IsNullOrEmpty(row.RoomUUID))
                            row.ViewModelStatus = RecordActions.Inserted;
                        //checks
                        if (row.ViewModelStatus == RecordActions.Inserted || row.ViewModelStatus == RecordActions.Updated)
                        {
                            SettledListModel tempSettle = settledAllListForChecks.Where(t => t.UUID == row.UUID).FirstOrDefault();
                            if (tempSettle != null)
                                tempSettle = row;
                            else
                                settledAllListForChecks.Add(row);

                            if (settledAllListForChecks.Where(t => t.RoomNumber == row.RoomNumber).Count() > 4)
                            {
                                MessageBox.Show("В комнате " + row.RoomNumber + " теперь больше четырех человек");
                                MakeDataGridRowFocused(dg, count);
                                return;
                            }
                            string roomSex = settledAllListForChecks.Where(t => t.RoomNumber == row.RoomNumber && t.PersonUUID != row.PersonUUID).Select(t => t.Sex).Distinct().ElementAtOrDefault(0);
                            if (String.IsNullOrEmpty(roomSex)==false && roomSex!=row.Sex)
                            {
                                MessageBox.Show("В одной команте не могут быть жители разного пола");
                                MakeDataGridRowFocused(dg, count);
                                return;
                            }
                            if (settledAllListForChecks.Where(t => t.PersonUUID == row.PersonUUID).Count() > 1)
                            {
                                MessageBox.Show("Человек селится больше, чем в одну комнату");
                                MakeDataGridRowFocused(dg, count);
                                return;
                            }
                            row.SettledDate = DateTime.Now;
                            if (String.IsNullOrEmpty(row.UUID))
                                row.UUID = Guid.NewGuid().ToString();
                            row.RoomUUID = newRoomUUID;
                            dbService.HandleSettledListTable(row, t => (t.UUID == row.UUID), row.ViewModelStatus);
                            savef = true;
                        }
                        count++;
                    }
                    if (savef)
                    {
                        MessageBox.Show("Saved");
                        int result = dbService.SaveChanges();
                        if (result > 0)
                        {
                            ms.Position = 0;
                            settledAllList = settledAllListForChecks;
                            oldSettleAllListKeeper.Serialize(ms, settledAllList);
                        }
                        else
                            MessageBox.Show("Люди не заселены");
                    }
                }
            }  
        }

        public void UnSettleButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.GetType() != typeof(DataGrid))
            {
                MessageBox.Show("Невозможно определить список");
                return;
            }
            var dg = sender as DataGrid;
            if (dg == null)
            {
                MessageBox.Show("Невозможно определить список");
                return;
            }
            else
            {
                var toUnSettleList = dg.Items.OfType<SettledListModel>();
                int count = 0;

                using (DataBaseConnector dbService = new DataBaseConnector())
                {
                    ObservableCollection<SettledListModel> settledAllListForChecks = new ObservableCollection<SettledListModel>();
                    if (ms != null)
                    {
                        ms.Position = 0;
                        settledAllListForChecks = (ObservableCollection<SettledListModel>)oldSettleAllListKeeper.Deserialize(ms);
                    }
                    bool savef = false;
                    foreach (var row in toUnSettleList)
                    {
                        if (String.IsNullOrEmpty(row.RoomUUID) == false)
                            row.ViewModelStatus = RecordActions.Deleted;
 
                        //checks
                        if (row.ViewModelStatus == RecordActions.Deleted)
                        {
                            SettledListModel tempSettle = settledAllListForChecks.Where(t => t.UUID == row.UUID).FirstOrDefault();
                            if (tempSettle != null)
                                tempSettle = row;
                            else
                            {
                                MessageBox.Show("Нельзя выселить. Неопредленная запись");
                                MakeDataGridRowFocused(dg, count);
                                return;
                            }
                            if (String.IsNullOrEmpty(row.UUID))
                            {
                                MessageBox.Show("Нельзя выселить. Неопредленная запись");
                                MakeDataGridRowFocused(dg, count);
                                return;
                            }
                            row.RoomNumber = "";
                            row.SettledDate = DateTime.MinValue;
                            row.RoomUUID = "";
                            settledAllListForChecks.Remove(row);
                            dbService.HandleSettledListTable(row, t => (t.UUID == row.UUID), row.ViewModelStatus);
                            savef = true;
                        }
                        count++;
                    }
                    if (savef)
                    {
                        MessageBox.Show("Saved");
                        int result = dbService.SaveChanges();
                        if (result > 0)
                        {
                            ms.Position = 0;
                            settledAllList = settledAllListForChecks;
                            oldSettleAllListKeeper.Serialize(ms, settledAllList);
                        }
                        else
                            MessageBox.Show("Люди не выселены");
                    }
                }
            }  
        }
        public void RelocationManagerView_Closing(object sender, CancelEventArgs e)
        {
            ms.Close();
        }
    }
}
