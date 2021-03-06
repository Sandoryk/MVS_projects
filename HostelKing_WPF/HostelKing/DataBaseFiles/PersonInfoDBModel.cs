﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HostelKing
{
    public class PersonInfoDBModel : IPersonInfo, IDataBaseModel
    {
        public int Id { get; set; }
        [Required]
        public string UUID { get; set; }
        [Required, StringLength(50)]
        public string FirstName { get; set; }
        [Required, StringLength(50)]
        public string LastName { get; set; }
        public DateTime DateBirth { get; set; }
        public string RoomUUID { get; set; }
        public string Sex { get; set; }
    }
}
