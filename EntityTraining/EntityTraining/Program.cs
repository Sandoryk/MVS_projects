using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTraining
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new EFContext();
            var item1 = new Item { Description = "Phone", SerialNumber = "000001" };
            var item2 = new Item { Description = "NoteBook", SerialNumber = "000002" };
            var cust = new Customer { Name = "RetailCust" };
            var order = new Order { TransDate = DateTime.Now };
            db.Items.Add(item1);
            db.Items.Add(item2);
            db.Customers.Add(cust);
            db.Orders.Add(order);
            db.SaveChanges();
            order = db.Orders.FirstOrDefault();
            item1 = db.Items.FirstOrDefault();
            var sl = new SalesLedger { ItemId = item1.ItemId, OrderId = order.OrderId };
            db.SalesLedgers.Add(sl);
            db.SaveChanges();
        }
    }
}
