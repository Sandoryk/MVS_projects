using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receipt
{
    class Program
    {
        PosReceipt receipt;
        static void Main(string[] args)
        {
            Program prog = new Program();
            prog.StrartProgram();
        }

        public void StrartProgram()
        {
            receipt = new PosReceipt("Kyiv, Volodymyrska str, 25", "Thanks for your choice");
            receipt.openreceipt += receipt_openreceipt;
            receipt.closereceipt += receipt_closereceipt;

            receipt.AddItem(new Item("Butter", 28, 10));
            receipt.AddItem(new Item("Coffee", 85, 5));
            receipt.AddItem(new ItemWithRebate("Lemon", 45.5, 3, 15));
            receipt.AddItem(new ItemWithTax("Beer", 65, 1, "20%"));
            receipt.AddPayment(1200);
            /*receipt.ClearUpCheck();
            receipt.Print(null);*/
            Console.ReadLine();
        }

        void receipt_closereceipt(object sender, EventArgs e)
        {
            ReceiptEventArgs e1 = e as ReceiptEventArgs;
            Console.WriteLine("Action: " + e1.OperationName + " on " + e1.OperationTime );
            Console.WriteLine("");
        }

        void receipt_openreceipt(object sender, EventArgs e)
        {
            ReceiptEventArgs e1 = e as ReceiptEventArgs;
            Console.WriteLine("Action: " + e1.OperationName + " on " + e1.OperationTime);
            Console.WriteLine("");
        }
    }

}
