﻿using System;
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
    public class PersonPaymentsModel: IPersonPayments, INotifyPropertyChanged
    {
        private string personId;
        private DateTime fromDate;
        private DateTime toDate;
        private double sum;

        public PersonPaymentsModel()
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
                if (personId != value)
                {
                    personId = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("PersonId");
                }
            }
        }
        public DateTime FromDate
        {
            get { return fromDate; }
            set
            {
                if (fromDate != value)
                {
                    fromDate = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("FromDate");
                }
            }
        }
        public DateTime ToDate
        {
            get { return toDate; }
            set
            {
                if (toDate != value)
                {
                    toDate = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("ToDate");
                }
            }
        }
        public double Sum
        {
            get { return sum; }
            set
            {
                if (sum != value)
                {
                    sum = value;
                    if (ViewModelStatus != RecordActions.Inserted)
                    {
                        ViewModelStatus = RecordActions.Updated;
                    }
                    OnPropertyChanged("Sum");
                }
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
