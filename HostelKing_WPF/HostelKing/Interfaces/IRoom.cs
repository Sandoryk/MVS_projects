using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public interface IRoom
    {
        int Id { get; set; }
        string UUID { get; set; }
        string RoomNumber { get; set; }
    }
}
