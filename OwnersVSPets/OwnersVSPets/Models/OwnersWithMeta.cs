using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OwnersVSPets.Models
{
    public class OwnersWithMeta
    {
        public Meta Meta { get; set; }
        public List<Owner> OwnerList { get; set; }
    }
}