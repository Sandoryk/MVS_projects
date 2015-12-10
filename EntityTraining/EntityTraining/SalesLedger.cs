using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityTraining
{
    class SalesLedger
    {
        [Key]
        public int ItemId { get; set; }
        [Key]
        public int OrderId { get; set; }
    }
}
