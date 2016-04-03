using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Topic_3_Task_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void ClearUpList()
        {
            ListView.ListViewItemCollection collection = listView1.Items;
            if (collection.Count != 0)
            {
                foreach (var item in collection)
                {
                    listView1.Items.Remove((ListViewItem)item);
                }
            }
        }
        public void MakeSearch()
        {
            if (string.IsNullOrEmpty(this.textBox2.Text) == false)
            {
                int foundcount = 0;
                List<ListViewItem> list;

                list = new List<ListViewItem>();
                ClearUpList();
                Match curmatch = Regex.Match(this.textBox1.Text, this.textBox2.Text);
                while (curmatch.Success)
                {
                    foundcount += 1;
                    ListViewItem curitem = new ListViewItem(curmatch.ToString());
                    curitem.SubItems.Add(curmatch.Index.ToString());
                    list.Add(curitem);
                    curmatch = curmatch.NextMatch();
                }
                if (foundcount == 0)
                {
                    listView1.Items.Add("No matches");
                }
                else
                {
                    ListViewItem curitem = new ListViewItem("Matches: " + foundcount);
                    curitem.Font = new Font(curitem.Font, curitem.Font.Style | FontStyle.Italic);
                    listView1.Items.Add(curitem);
                    foreach (var item in list)
                    {
                        listView1.Items.Add(item);
                    }
                }
            }
            else
            {
                ClearUpList();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MakeSearch();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter) 
            {
                MakeSearch();
            }
        }
    }
}
