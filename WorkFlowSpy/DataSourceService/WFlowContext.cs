using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSourceService.DBModels;

namespace DataSourceService
{
    public class WFlowContext : DbContext
    {
        public WFlowContext(string connectionStr)
            : base(connectionStr)
        {
        }
        public DbSet<TaskDB> Tasks { get; set; }
        public DbSet<LinkDB> Links { get; set; }
        //public DbSet<SupplierDL> Suppliers { get; set; }
    }
}
