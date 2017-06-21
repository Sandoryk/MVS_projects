using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.Models
{
    public class Owner
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int PetsAmount { get; set; }
    }
}