using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class PersonInfoEventArgs : EventArgs
    {
        public IPersonInfo PersonInfo { get; set; }
        public PersonInfoEventArgs(IPersonInfo pInfo)
        {
            PersonInfo = pInfo;
        }
    }
}
