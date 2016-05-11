using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Collections.ObjectModel;

namespace HostelKing
{
    [Serializable]
    public class PersonInfoViewModel : IPersonInfo, INotifyPropertyChanged
    {
        string firstName;
        string lastName;
        DateTime dateBirth;
        string roomNumber;
        string sex;
        ObservableCollection<PersonPaymentsViewModel> payments;

        public PersonInfoViewModel()
        {
            ViewModelStatus = RecordActions.NotModified;
            DateBirth = DateTime.Now;
        }

        public int Id { get; set; }
        public string UUID { get; set; }
        public RecordActions ViewModelStatus { get; set; }
        public string FirstName 
        {
            get { return firstName; } 
            set
            {
                firstName = value;
                if (ViewModelStatus!=RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;    
                }
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("LastName");
            }
        }
        public DateTime DateBirth
        {
            get { return dateBirth; }
            set
            {
                dateBirth = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("DateBirth");
            }
        }
        public string RoomNumber
        {
            get { return roomNumber; }
            set
            {
                roomNumber = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("RoomNumber");
            }
        }
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("Sex");
            }
        }
        public ObservableCollection<PersonPaymentsViewModel> Payments 
        {
            get { return payments;}
            set
            {
                payments = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("Payments");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
