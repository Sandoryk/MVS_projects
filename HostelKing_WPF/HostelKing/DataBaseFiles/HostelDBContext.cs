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
        public HostelDBContext()
            : base("name=HostelDBContext")
        {
        }
        public HostelDBContext(int timeout)
            : base("name=HostelDBContext") 
        {
            if (timeout>=5)
            {
                var objectContext = (this as IObjectContextAdapter).ObjectContext;
                objectContext.CommandTimeout = timeout;
            }
            
        }
        public DbSet<PersonInfoDBModel> PersonInfoList { get; set; }
        public DbSet<PersonPaymentsDBModel> PersonPaymentsList { get; set; }
        public DbSet<UserDBModel> UserList { get; set; }
        public DbSet<RoomDBModel> RoomList { get; set; }
        public DbSet<SettledListDBModel> SettledList { get; set; }
        public DbSet<RoomFurnitureDBModel> RoomFurnitureList { get; set; }
    }
}
