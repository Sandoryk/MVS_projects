using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class SettledListDBModel : ISettledList, IDataBaseModel
    {
        public int Id { get; set; }
        public string UUID { get; set; }
        public string PersonUUID { get; set; }
        public string RoomUUID { get; set; }
        public DateTime SettledDate { get; set; }
    }
}
