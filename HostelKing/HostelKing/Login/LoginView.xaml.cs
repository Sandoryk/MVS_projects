using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
                //FullFillDBWithRooms();
                //FullFillDBWithFurniture();
                try
                {
                    using (DataBaseConnector dbService = new DataBaseConnector(5))
                    {
                        PersonInfoModel viewPerson;
                        IRoom dbRoom;
                        List<IRoom> tempRoomList = new List<IRoom>();
                        PersonInfoListView hb = new PersonInfoListView();
                        ObservableCollection<PersonInfoModel> viewPersons = new ObservableCollection<PersonInfoModel>();

                        List<IPersonInfo> dbPersons = dbService.GetAllRecords<IPersonInfo>();
                        List<IRoom> dbRoomList = dbService.GetAllRecords<IRoom>();
                        
                        PropertyInfo[] propInfos = typeof(IPersonInfo).GetProperties();
                        foreach (var dbPer in dbPersons)
                        {
                            viewPerson = new PersonInfoModel();
                            foreach (var curPropt in propInfos)
                            {
                                curPropt.SetValue(viewPerson, curPropt.GetValue(dbPer));
                            }
                            tempRoomList = dbRoomList.Where(t => t.UUID == viewPerson.RoomUUID).ToList();
                            if (tempRoomList.Count>0)
	                        {
                                dbRoom = tempRoomList[0];
                                viewPerson.RoomNumber = dbRoom.RoomNumber;
	                        }
                            viewPersons.Add(viewPerson);
                        }
                        PersonInfoListViewModel hbViewModel = new PersonInfoListViewModel(viewPersons);
                        hb.NewButton.Click += hbViewModel.NewButton_Click;
                        hb.ManagerButton.Click += hbViewModel.ManagerButton_Click;
                        hb.SchemeButton.Click += hbViewModel.SchemeButton_Click;
                        hb.Redirect_HabitantsGridRow_DoubleClick += hbViewModel.Row_DoubleClick;
                        hb.Redirect_HabitantsGridRow_MouseLeftButtonDown += hbViewModel.Row_MouseLeftButtonDown;
                        hb.RefreshButton.Click += hbViewModel.RefreshButton_Click;
                        hb.DataContext = hbViewModel;
                        hb.Show();
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    this.Close();
                }
            }
        }
        private void FullFillDBWithRooms()
        {
            IRoom room = new RoomDBModel();;
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 12; j++)
                    {
                        room.UUID = Guid.NewGuid().ToString();
                        room.RoomNumber = (i + 1) + "-" + (j + 1);
                        dbService.HandleRoomsTable(room, null, RecordActions.Inserted);
                    }
                }
                dbService.SaveChanges();
            }
        }
        private void FullFillDBWithFurniture()
        {
            IRoomFurniture roomfurn;
            List<IRoom> rooms = new List<IRoom>();
            
            using (DataBaseConnector dbService = new DataBaseConnector())
            {

                rooms = dbService.GetAllRecords<IRoom>();
                foreach ( var room in rooms)
                {

                    roomfurn = new RoomFurnitureDBModel();
                    roomfurn.UUID = Guid.NewGuid().ToString();
                    roomfurn.RoomUUID = room.UUID;
                    roomfurn.FurnitureUnit = "Кровать";
                    roomfurn.Quantity = 2;
                    dbService.HandleRoomFurnituresTable(roomfurn,null,RecordActions.Inserted);

                    roomfurn.UUID = Guid.NewGuid().ToString();
                    roomfurn.FurnitureUnit = "Стул";
                    roomfurn.Quantity = 2;
                    dbService.HandleRoomFurnituresTable(roomfurn, null, RecordActions.Inserted);

                    roomfurn.UUID = Guid.NewGuid().ToString();
                    roomfurn.FurnitureUnit = "Стол";
                    roomfurn.Quantity = 1;
                    dbService.HandleRoomFurnituresTable(roomfurn, null, RecordActions.Inserted);

                    roomfurn.UUID = Guid.NewGuid().ToString();
                    roomfurn.FurnitureUnit = "Шкаф";
                    roomfurn.Quantity = 2;
                    dbService.HandleRoomFurnituresTable(roomfurn, null, RecordActions.Inserted);

                    roomfurn.UUID = Guid.NewGuid().ToString();
                    roomfurn.FurnitureUnit = "Полочка";
                    roomfurn.Quantity = 3;
                    dbService.HandleRoomFurnituresTable(roomfurn, null, RecordActions.Inserted);
                }
                dbService.SaveChanges();
            }
        }
    }
}
