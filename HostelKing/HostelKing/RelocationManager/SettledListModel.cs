using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    [Serializable]
    public class SettledListModel : ISettledList, INotifyPropertyChanged
    {
        DateTime sattledDate;
        string roomNumber;

        public SettledListModel()
        {
            ViewModelStatus = RecordActions.NotModified;
        }

        public int Id { get; set; }
        public string UUID { get; set; }
        public string PersonUUID { get; set; }
        public string RoomUUID { get; set; }
        public string Sex { get; set; }
        public RecordActions ViewModelStatus { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime SettledDate
        {
            get { return sattledDate; }
            set
            {
                if (sattledDate != value)
                {
                    sattledDate = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("SettledDate");
                }
            }
        }
        public string RoomNumber
        {
            get { return roomNumber; } 
            set
            {
                if (roomNumber != value)
                {
                    roomNumber = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("RoomNumber");
                }
            }
        }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
