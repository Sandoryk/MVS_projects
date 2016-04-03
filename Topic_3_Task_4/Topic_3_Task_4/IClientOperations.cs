using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Topic_3_Task_4
{
    interface IClientOperations <T>
    {
    List<T> DataList{get;set;}
    void SortItems(ListView list,int column);
    string MakeSearch(ListView list,string inputstr);
    }
}
