using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace HostelKing
{
    public class HostelDBContext:DbContext
    {
        ObjectContext objectContext;
        public HostelDBContext()
            : base("name=HostelDBContext") 
        {
            objectContext = (this as IObjectContextAdapter).ObjectContext;
        }
        public int? OperationTimeOut
        {
            get {return objectContext.CommandTimeout; }
            set
            {
                if (value>=5 || value==null)
                {
                    objectContext.CommandTimeout = value;
                };
            }
        }
        public DbSet<PersonInfoDBModel> PersonInfoList { get; set; }
        public DbSet<PersonPaymentsDBModel> PersonPaymentsList { get; set; }
    }
}
