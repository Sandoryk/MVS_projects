﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLevel
{
    [Table("Suppliers")]
    public class SupplierDL
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }
}
