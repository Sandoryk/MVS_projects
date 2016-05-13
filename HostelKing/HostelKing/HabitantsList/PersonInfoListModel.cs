using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class PersonInfoListModel
    {
        public event PersonChangedEventHandler OnPersonInfoChanged;
        public PersonInfoListModel(ObservableCollection<IPersonInfo> list)
        {
            Habitants = list;
        }
        public ObservableCollection<IPersonInfo> Habitants { get; set; }

        /*protected void RunPersonInfoChanged(IPersonInfo pInfo)
        {
            if (OnPersonInfoChanged != null)
                PropertyChanged(this, new PersonInfoEventArgs(new PersonInfoView()));
        }*/

    }
}
