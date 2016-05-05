using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class PersonInfoListViewModel
    {
        public PersonInfoListViewModel(ObservableCollection<IPersonInfo> list)
        {
            Habitants = list;
        }
        public ObservableCollection<IPersonInfo> Habitants { get; set; }

    }
}
