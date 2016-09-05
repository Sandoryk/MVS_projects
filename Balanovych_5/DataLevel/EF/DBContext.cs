using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    public class DBContext : DbContext
    {
        public DBContext(string con)
            : base(con)
        {
        }
        public DbSet<ItemDL> Items { get; set; }
        public DbSet<ItemGroupDL> ItemGroups { get; set; }
        public DbSet<SupplierDL> Suppliers { get; set; }
    }
}
