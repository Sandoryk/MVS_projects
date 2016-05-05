using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class PersonPaymentsDBModel : IPersonPayments, IDataBaseModel
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public double Sum { get; set; }
    }
}
