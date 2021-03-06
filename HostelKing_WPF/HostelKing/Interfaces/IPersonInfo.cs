﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostelKing
{
    public interface IPersonInfo
    {
        int Id { get; set; }
        string UUID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateBirth { get; set; }
        string RoomUUID { get; set; }
        string Sex { get; set; }
    }
}
