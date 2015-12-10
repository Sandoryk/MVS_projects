using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace EntityTraining
{
    class Order
    {
        [Key]
        public int OrderId { get; set; }
        [Timestamp]
        public DateTime TransDate { get; set; }
        public int CustId { get; set; }
        public virtual List<SalesLedger> SalesLedgers { get; set; }
    }
}
