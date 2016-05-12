using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace HostelKing
{
    [Serializable]
    public class PersonPaymentsViewModel: IPersonPayments, INotifyPropertyChanged
    {
        private string personId;
        private DateTime fromDate;
        private DateTime toDate;
        private double sum;

        public PersonPaymentsViewModel()
        {
            fromDate = DateTime.Now;
            toDate = fromDate;
        }
        public int Id { get; set; }

        public RecordActions ViewModelStatus { get; set; }
        public string UUID { get; set; }
        public string PersonUUID
        {
            get { return personId; }
            set
            {
                personId = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("PersonId");
            }
        }
        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                fromDate = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("FromDate");
            }
        }
        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                toDate = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("ToDate");
            }
        }
        public double Sum
        {
            get { return sum; }
            set
            {
                sum = value;
                if (ViewModelStatus != RecordActions.Inserted)
                {
                    ViewModelStatus = RecordActions.Updated;
                }
                OnPropertyChanged("Sum");
            }
        }
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) 
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
