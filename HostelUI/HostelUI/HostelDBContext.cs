﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace HostelUI
{
    class HostelDBContext:DbContext
    {
        public HostelDBContext()
            : base("name=HostelDBContext") 
        { 
        }
        public DbSet<PersonInfo> PersonInfoList { get; set; }
    }
}
