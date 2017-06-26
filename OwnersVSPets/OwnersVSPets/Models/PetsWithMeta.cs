using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.Models
{
    public class PetsWithMeta
    {
        public Meta Meta { get; set; }
        public List<Pet> PetList { get; set; }
    }
}