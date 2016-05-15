using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public class UserDBModel : IUser, IDataBaseModel
    {
        public int Id { get; set; }
        public string PersonUUID { get; set; }
        public bool IsActive { get; set; }
        public string Login { get; set; }
        public string Password { get; set; } 
        public string AccessGroup { get; set; } 
    }
}
