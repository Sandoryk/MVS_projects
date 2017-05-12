using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public interface IRoomFurniture
    {
        int Id { get; set; }
        string UUID { get; set; }
        string RoomUUID { get; set; }
        string FurnitureUnit { get; set; }
        int Quantity { get; set; }
    }
}
