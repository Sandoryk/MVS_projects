using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class PersonPaymentsDBModel : IPersonPayments, IDataBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string UUID { get; set; }
        public string PersonUUID { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Sum { get; set; }
    }
}
