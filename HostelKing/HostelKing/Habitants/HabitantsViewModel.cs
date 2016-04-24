using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class HabitantsViewModel
    {
        public HabitantsViewModel(ObservableCollection<PersonInfo> list)
        {
            Habitants = list;
        }
        public ObservableCollection<PersonInfo> Habitants { get; set; }

    }
}
