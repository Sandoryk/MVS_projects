using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Topic_4_Task_1
{
    class ClientOprations : IClientOperations<ClientInfo>
    {
        public ClientOprations()
        {
            DataList = new List<ClientInfo>();
        }
        public List<ClientInfo> DataList { get; set; }
        public void SortItems(ListView list, int column)
        {
            if (list.Sorting == SortOrder.Ascending)
            {
                list.Sorting = SortOrder.Descending;
            }
            else
            {
                list.Sorting = SortOrder.Ascending;
            }
            list.ListViewItemSorter = new ClientComparer(column, list.Sorting);
            return;
        }
        public string MakeSearch(ListView list,string inputstr)
        {
            string resstr = "";

            if (string.IsNullOrEmpty(inputstr) == false)
            {
                string formatch = "";

                foreach (var item in list.Items)
                {
                    for (int i = 0; i < list.Columns.Count; i++)
                    {
                        formatch += ((ListViewItem)item).SubItems[i].ToString();
                    }
                    formatch += "\n";

                }
                MatchCollection matches = Regex.Matches(formatch, inputstr);
                if (matches.Count > 0)
                {
                    foreach (var match in matches)
                    {
                        resstr += (match.ToString() + "\n");
                    }
                }
            }
            return resstr;
        }
    }
}
