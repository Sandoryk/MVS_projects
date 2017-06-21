using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.Models
{
    public class DataContext
    {
        List<Owner> owners;
        public DataContext()
        {
            owners = new List<Owner>();
            owners.Add(new Owner { ID = 0, Name = "Tom" });
            owners.Add(new Owner { ID = 1, Name = "Sam" });
            owners.Add(new Owner { ID = 2, Name = "Bill" });
            owners.Add(new Owner { ID = 3, Name = "Jack" });
            owners.Add(new Owner { ID = 4, Name = "Jim" });
        }
        public List<Owner> GetOwners 
        {
            get { return owners; }
        }
    }
}