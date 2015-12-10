using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EntityTraining
{
    class EFContext : DbContext 
    {
        public EFContext() 
            : base("name=EFContext") 
        { 
        } 
        public DbSet<Item> Items { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SalesLedger> SalesLedgers { get; set; }
    }
}
