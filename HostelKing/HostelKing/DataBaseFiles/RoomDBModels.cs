using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class RoomDBModel : IRoom, IDataBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string UUID { get; set; }
        public string RoomNumber { get; set; }
    }
}
