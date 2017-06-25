using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace OwnersVSPets.DAL
{
    public class OwnersPetsContext : DbContext
    {
        public OwnersPetsContext(string connectionStr)
            : base(connectionStr)
        {
        }
        
        public DbSet<DBOwner> Owners { get; set; }
        public DbSet<DBPet> Pets { get; set; }
        
    }
}