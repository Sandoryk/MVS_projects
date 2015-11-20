using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receipt
{
    public class ReceiptEventArgs : EventArgs
    {
        public string OperationName { get; set; }
        public DateTime OperationTime { get; set; }
    }

    public class PosReceipt
    {
        DateTime NullableDate;
        bool openedf = false; 
        List<Item> itemlist = new List<Item>();
        
        public event EventHandler openreceipt;
        public event EventHandler closereceipt;
        public event EventHandler notopenedreceipt;

        public PosReceipt(string adr, string custmes)
        {
            Address = adr;
            CustomerMesg = custmes;
            Total = 0;
            Change = 0;
            AbsRebate = 0;
            TimeStamp = NullableDate;
        }

        public string Address { get; set; }
        public double Total { get; set; }
        public double Change { get; set; }
        public double AbsRebate { get; set; }
        public string CustomerMesg { get; set; }
        public DateTime TimeStamp { get; set; }

        public void ClearUpCheck()
        {
            Total = 0;
            Change = 0;
            AbsRebate = 0;
            TimeStamp = NullableDate;

        }
        
        public bool AddItem(Item newitem)
        {
            bool res = true;

            if (openedf && itemlist.Count > 0 || !openedf && itemlist.Count == 0)
            {
                if (openedf == false)
                {
                    openedf = true;
                    OnOpenReceipt();
                }
                itemlist.Add(newitem);

                //////////////////////////////
            }
                
            else
                res = false;

            return res;
        }

        public bool AddPayment(double cash)
        {
            bool res = true;
            double sum = 0, rebate = 0;

            if (openedf)
            {
                foreach (Item item in itemlist)
                {
                    sum += item.Price * item.Quantity;
                    rebate += (item.Price * item.Quantity)/100*item.Rebate;
                }
                if (cash >= (sum - rebate))
                {
                    Total = sum - rebate;
                    Change = cash - (sum - rebate);
                    AbsRebate = rebate;
                    TimeStamp = DateTime.Now;
                    openedf = false;
                    itemlist.RemoveRange(0,itemlist.Count);
                    OnCloseReceipt();
                }
                else
                    res = false;
            }
            else
            {
                res = false;
                OnNotOpenedReceipt();
            }

            return res;
        }

        public void OnOpenReceipt()
        {
            if (openreceipt != null)
            {
                ReceiptEventArgs e = new ReceiptEventArgs();
                e.OperationName = "Receipt is opened";
                e.OperationTime = DateTime.Now;
                openreceipt(this, e);
            }
        }

        private void OnCloseReceipt()
        {
            if (closereceipt != null)
            {
                ReceiptEventArgs e = new ReceiptEventArgs();
                e.OperationName = "Receipt is closed";
                e.OperationTime = DateTime.Now;
                closereceipt(this, e);
            }
        }

        private void OnNotOpenedReceipt()
        {
            if (notopenedreceipt != null)
            {
                ReceiptEventArgs e = new ReceiptEventArgs();
                e.OperationName = "Receipt is not open";
                e.OperationTime = DateTime.Now;
                notopenedreceipt(this, e);
            }
        }
    }

    public static class Extensions
    {
        public static void Print(this PosReceipt pos)
        {
            Console.WriteLine(pos.Address);
            Console.WriteLine("----------------");
            Console.WriteLine("TOTAL:  " + pos.Total);
            Console.WriteLine("Rebate: " + pos.AbsRebate);
            Console.WriteLine("Change: " + pos.Change);
            Console.WriteLine(" ");
            Console.WriteLine("On date: " + pos.TimeStamp);
            Console.WriteLine("----------------");
            Console.WriteLine("To customer: " + pos.CustomerMesg);
            Console.WriteLine("----------------");
        }
    }  
}
