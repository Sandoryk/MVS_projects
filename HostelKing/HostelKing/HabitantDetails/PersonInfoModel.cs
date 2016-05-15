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
    public class PersonInfoModel : IPersonInfo, INotifyPropertyChanged
    {
        string firstName;
        string lastName;
        DateTime dateBirth;
        string sex;
        ObservableCollection<PersonPaymentsModel> payments;

        public PersonInfoModel()
        {
            ViewModelStatus = RecordActions.NotModified;
            DateBirth = DateTime.Now;
            payments = new ObservableCollection<PersonPaymentsModel>();
        }

        public int Id { get; set; }
        public string UUID { get; set; }

        public string RoomNumber { get; set; }
        public DateTime SettledDate { get; set; }
        public RecordActions ViewModelStatus { get; set; }
        public string FirstName 
        {
            get { return firstName; } 
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("FirstName");
                }
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("LastName");
                }
            }
        }
        public DateTime DateBirth
        {
            get { return dateBirth; }
            set
            {
                if (dateBirth != value)
                {
                    dateBirth = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("DateBirth");
                }
            }
        }
        public string RoomUUID { get; set; }
        public string Sex
        {
            get { return sex; }
            set
            {
                if (sex != value)
                {
                    sex = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("Sex");
                }
            }
        }
        public ObservableCollection<PersonPaymentsModel> Payments 
        {
            get { return payments;}
            set
            {
                if (payments != value)
                {
                    payments = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("Payments");
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
