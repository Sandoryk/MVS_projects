using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    interface IPersonPayments
    {
        int Id { get; set; }
        int PersonId { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        double Sum { get; set; }
    }
}
