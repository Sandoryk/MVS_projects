using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HostelKing
{
    public class PersonPaymentsViewModel: IPersonPayments, INotifyPropertyChanged
    {
        public bool viewModelIsChanged = false;
        private int personId;
        private DateTime fromDate;
        private DateTime toDate;
        private double sum;

        public int Id { get; set; }
        public int PersonId
        {
            get { return personId; }
            set
            {
                personId = value;
                viewModelIsChanged = true;
                OnPropertyChanged("PersonId");
            }
        }
        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                fromDate = value;
                viewModelIsChanged = true;
                OnPropertyChanged("FromDate");
            }
        }
        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                toDate = value;
                viewModelIsChanged = true;
                OnPropertyChanged("ToDate");
            }
        }
        public double Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                viewModelIsChanged = true;
                OnPropertyChanged("Sum");
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
