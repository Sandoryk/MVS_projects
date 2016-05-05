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

namespace HostelKing
{
    public class PersonInfoViewModel : IPersonInfo, INotifyPropertyChanged
    {
        private bool viewModelIsChanged = false;
        private string firstName;
        private string lastName;
        private DateTime dateBirth;
        private string roomNumber;
        private string sex;
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
