using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Topic_4_Task_1
{
    public partial class Form1 : Form
    {
        ClientOprations clientop;
        public Form1()
        {
            InitializeComponent();
            clientop = new ClientOprations();
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
        public void RebuildList()
        {
            if (clientop.DataList.Count != 0)
            {
                foreach (var client in clientop.DataList)
                {
                    ListViewItem curitem = new ListViewItem(client.GetProperties());
                    listView1.Items.Add(curitem);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ClearUpList();
            if (clientop.DataList.Count>0)
            {
                clientop.DataList = new List<ClientInfo>();
            }
            ClientInfo client1 = new ClientInfo("Сергей", "Гореликов", DateTime.ParseExact("06/02/1985", "dd/MM/yyyy", CultureInfo.InvariantCulture), "+380 97 125 88 11", "mail1@gmail.com", "СТ 985642", "1236456978950315", DateTime.Now);
            clientop.DataList.Add(client1);
            ClientInfo client2 = new ClientInfo("Иван", "Куценко", DateTime.ParseExact("14/12/1991", "dd/MM/yyyy", CultureInfo.InvariantCulture), "+380 97 564 77 90", "mail2@gmail.com", "СТ 672752", "7527527527427874", DateTime.Now);
            clientop.DataList.Add(client2);
            ClientInfo client3 = new ClientInfo("Леонид", "Куравлев", DateTime.ParseExact("23/08/1987", "dd/MM/yyyy", CultureInfo.InvariantCulture), "+380 97 567 33 11", "mail3@gmail.com", "СТ 727757", "9606275782578212", DateTime.Now);
            clientop.DataList.Add(client3);
            ClientInfo client4 = new ClientInfo("Вениамин", "Катников", DateTime.ParseExact("05/10/1975", "dd/MM/yyyy", CultureInfo.InvariantCulture), "+380 97 084 43 99", "mail4@gmail.com", "СТ 729275", "7936012743700457", DateTime.Now);
            clientop.DataList.Add(client4);
            ClientInfo client5 = new ClientInfo("Тихон", "Корнийчук", DateTime.ParseExact("30/05/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture), "+380 97 765 75 23", "mail5@gmail.com", "СТ 164513", "7522719131757378", DateTime.Now);
            clientop.DataList.Add(client5);
            RebuildList();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            clientop.SortItems(listView1, e.Column);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = clientop.MakeSearch(listView1, textBox1.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string fn = "", ln = "", phone = "", email = "", passport = "", cardID = "";
            DateTime bd, cardd;
            bool insertf;

            insertf = true;
            bd = dateTimePicker1.Value;
            cardd = dateTimePicker2.Value;
            if (String.IsNullOrEmpty(textBox3.Text)==true)
            {
                MessageBox.Show("Пустое поле");
                insertf = false;
                textBox3.Focus();
                goto Skip;  
            }
            if (Regex.IsMatch(textBox3.Text, "^[A-zА-я]+$") == false)
            {
                MessageBox.Show("Шаблон: [A-zА-я]+");
                insertf = false;
                textBox3.Focus();
                goto Skip;
            }
            else
                fn = textBox3.Text;
            if (String.IsNullOrEmpty(textBox4.Text) == true)
            {
                MessageBox.Show("Пустое поле");
                insertf = false;
                textBox4.Focus();
                goto Skip;
            }
            if (Regex.IsMatch(textBox4.Text, "^[A-zА-я]+$") == false)
            {
                MessageBox.Show("Шаблон: [A-zА-я]+");
                insertf = false;
                textBox4.Focus();
                goto Skip;
            }
            else
                ln = textBox4.Text;
            if (String.IsNullOrEmpty(textBox5.Text) == true)
            {
                MessageBox.Show("Пустое поле");
                insertf = false;
                textBox5.Focus();
                goto Skip;
            }
            if (Regex.IsMatch(textBox5.Text, @"^\+380 \d\d \d\d\d \d\d \d\d$") == false)
            {
                MessageBox.Show(@"Шаблон: +380 \d\d \d\d\d \d\d \d\d");
                insertf = false;
                textBox5.Focus();
                goto Skip;
            }
            else
                phone = textBox5.Text;
            if (String.IsNullOrEmpty(textBox6.Text) == true)
            {
                MessageBox.Show("Пустое поле");
                insertf = false;
                textBox6.Focus();
                goto Skip;
            }
            if (Regex.IsMatch(textBox6.Text, "^[.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}$") == false)
            {
                MessageBox.Show("Шаблон: [.\\-_a-z0-9]+@([a-z0-9][\\-a-z0-9]+\\.)+[a-z]{2,6}");
                insertf = false;
                textBox6.Focus();
                goto Skip;
            }
            else
                email = textBox6.Text;
            if (String.IsNullOrEmpty(textBox7.Text) == true)
            {
                MessageBox.Show("Пустое поле");
                insertf = false;
                textBox7.Focus();
                goto Skip;
            }
            if (Regex.IsMatch(textBox7.Text, @"^[A-zА-я]{2} \d\d\d\d\d\d$") == false)
            {
                MessageBox.Show(@"Шаблон: [A-zА-я]{2} \d\d\d\d\d\d");
                insertf = false;
                textBox7.Focus();
                goto Skip;
            }
            else
                passport = textBox7.Text;
            if (String.IsNullOrEmpty(textBox8.Text) == true)
            {
                MessageBox.Show("Пустое поле");
                insertf = false;
                textBox8.Focus();
                goto Skip;
            }
            if (Regex.IsMatch(textBox8.Text, "^[0-9]{16}$") == false)
            {
                MessageBox.Show("Шаблон: [0-9]{16}");
                insertf = false;
                textBox8.Focus();
                goto Skip;
            }
            else
                cardID = textBox8.Text;

            Skip:;
            if (insertf)
            {
                ClientInfo newclient = new ClientInfo(fn, ln, bd, phone, email, passport, cardID,cardd);
                ListViewItem curitem = new ListViewItem(newclient.GetProperties());
                listView1.Items.Add(curitem);    
            }
        }
    }

}
