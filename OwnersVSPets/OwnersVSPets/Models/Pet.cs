using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.Models
{
    public class Pet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int OwnerID { get; set; }
    }
}