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
    public class PersonInfoViewModel : IPersonInfo, INotifyPropertyChanged
    {
        public bool viewModelIsChanged = false;
        string firstName;
        string lastName;
        DateTime dateBirth;
        string roomNumber;
        string sex;
        ObservableCollection<PersonPaymentsViewModel> payments;

        public int Id { get; set; }
        public string FirstName 
        {
            get { return firstName; } 
            set
            {
                firstName = value;
                viewModelIsChanged = true;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                lastName = value;
                viewModelIsChanged = true;
                OnPropertyChanged("LastName");
            }
        }
        public DateTime DateBirth
        {
            get { return dateBirth; }
            set
            {
                dateBirth = value;
                viewModelIsChanged = true;
                OnPropertyChanged("DateBirth");
            }
        }
        public string RoomNumber
        {
            get { return roomNumber; }
            set
            {
                roomNumber = value;
                viewModelIsChanged = true;
                OnPropertyChanged("RoomNumber");
            }
        }
        public string Sex
        {
            get { return sex; }
            set
            {
                sex = value;
                viewModelIsChanged = true;
                OnPropertyChanged("Sex");
            }
        }
        public ObservableCollection<PersonPaymentsViewModel> Payments 
        {
            get { return payments;}
            set
            {
                payments = value;
                viewModelIsChanged = true;
                OnPropertyChanged("Payments");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
