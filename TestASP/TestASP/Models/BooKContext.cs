using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;

namespace TestASP.Models
{
    public class BookContext:DbContext
    {
        public System.Data.Entity.DbSet<TestASP.Models.Book> Books { get; set; }
    }
}