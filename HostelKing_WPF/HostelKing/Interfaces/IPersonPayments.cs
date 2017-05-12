using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public interface IPersonPayments
    {
        int Id { get; set; }
        string UUID { get; set; }
        string PersonUUID { get; set; }
        DateTime FromDate { get; set; }
        DateTime ToDate { get; set; }
        double Sum { get; set; }
    }
}
