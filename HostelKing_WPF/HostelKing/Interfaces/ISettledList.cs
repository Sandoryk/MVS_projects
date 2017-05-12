using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public interface ISettledList
    {
        int Id { get; set; }
        string UUID { get; set; }
        string PersonUUID { get; set; }
        string RoomUUID { get; set; }
        DateTime SettledDate { get; set; }
    }
}
