using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Topic_4_Task_1
{
    class ClientComparer : System.Collections.IComparer
    {
        private int col;
        SortOrder order;
        public ClientComparer()
        {
            col = 0;
            order = SortOrder.Ascending;
        }
        public ClientComparer(int column, SortOrder inorder)
        {
            col = column;
            order = inorder;
        }
        public int Compare(object x, object y)
        {
            int orderdirection = 1;
            if (order == SortOrder.Descending)
                orderdirection = -1;

            return orderdirection * String.Compare(((ListViewItem)x).SubItems[col].Text, ((ListViewItem)y).SubItems[col].Text);
        }
    }
}
