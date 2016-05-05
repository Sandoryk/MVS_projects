using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HostelKing
{
    class HostelDBContext:DbContext
    {
        public HostelDBContext()
            : base("name=HostelDBContext") 
        { 
        }
        public DbSet<PersonInfoDBModel> PersonInfoList { get; set; }
        public DbSet<PersonPaymentsDBModel> PersonPaymentsList { get; set; }
    }
}
