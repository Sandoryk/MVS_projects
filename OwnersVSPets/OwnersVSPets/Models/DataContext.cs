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
        public bool CreateNewOwner(string newOwnerName)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(newOwnerName))
            {
                owners.Add(new Owner { ID = 6, Name = newOwnerName });
                result = true;
            }

            return result;
        }
        public bool DeleteOwner(string newOwnerName)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(newOwnerName))
            {
                Owner owner = owners.Find(t=>t.Name == newOwnerName);
                if (owner!=null)
                {
                    owners.Remove(owner);
                    result = true;
                }
            }

            return result;
        }
    }
}