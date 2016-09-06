﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    [Table("Items")]
    public class ItemDL
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ItemGroupID { get; set; }
        public string SupplierID { get; set; }
    }
}
