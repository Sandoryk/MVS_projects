using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    interface IUser
    {
        int Id { get; set; }
        string PersonUUID { get; set; }
        bool IsActive { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string AccessGroup { get; set; } 
    }
}
