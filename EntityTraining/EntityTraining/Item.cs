using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EntityTraining
{
    class Item
    {
        [Key]
        public int ItemId { get; set; }
        [Required]
        public string Description { get; set; }
        [Index("SerialIndex",IsUnique = true)]
        public string SerialNumber { get; set; }
        public double Price { get; set; }

        public virtual List<SalesLedger> SalesLedgers { get; set; }
    }
}
