using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Receipt
{
    public class  Item
    {
        public Item(string name, double price, double qty)
        {
            Name = name;
            Price = price;
            Quantity = qty;
            Rebate = 0;
            TaxGroup = "None";
        }
        public string Name { get; set; }
        public double Price { get; set; }
        public double Quantity { get; set; }
        public virtual double Rebate { get; set; }
        public virtual string TaxGroup { get; set; }

        public virtual void PrintItemDescription()
        {
            Console.WriteLine("Item info");
            Console.WriteLine("Name: " + Name);
            Console.WriteLine("Quantaty: " + Quantity);
            Console.WriteLine("Price: " + Price);
            Console.WriteLine("Rebate: " + Rebate);
            Console.WriteLine("TaxGroup: " + TaxGroup);
        }
    }

    class ItemWithRebate : Item
    {
        private double rebval;
        public ItemWithRebate(string name, double price, double qty, double rebate)
            : base(name,price,qty)
        {
            Name = name;
            Quantity = qty;
            Price = price;
            Rebate = rebate;
        }
        public override double Rebate
        {
            get { return rebval; }
            set
            {
                if (0 <= value && value <= 100)
                    rebval = value;
                else
                    rebval = 0;
            }
        }
    }

    class ItemWithTax : Item
    {

        private string taxgroup; 
        public ItemWithTax(string name, double price, double qty, string taxgroup)
            : base(name, price, qty)
        {
            Name = name;
            Quantity = qty;
            Price = price;
            TaxGroup = taxgroup;
        }
        public override string TaxGroup
        {
            get { return taxgroup; }
            set
            {
                if (value.ToString().Contains("%"))
                    taxgroup = value;
                else
                    taxgroup = "None";
            }
        }
    }

}
