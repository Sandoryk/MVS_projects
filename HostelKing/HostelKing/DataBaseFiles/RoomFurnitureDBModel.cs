using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class RoomFurnitureDBModel:IRoomFurniture,IDataBaseModel
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public string RoomUUID { get; set; }
        public string FurnitureUnit { get; set; }
        public int Quantity { get; set; }
    }
}
