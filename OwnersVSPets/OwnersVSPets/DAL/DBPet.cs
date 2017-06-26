using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.DAL
{
    public class DBPet
    {
        public Int64 ID { get; set; }
        public string Name { get; set; }
        public Int64 OwnerID { get; set; }
    }
}