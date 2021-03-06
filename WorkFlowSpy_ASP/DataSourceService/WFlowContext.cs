﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataSourceService.DBModels;

namespace DataSourceService
{
    public class WFlowContext : DbContext
    {
        public WFlowContext(string connectionStr)
            : base(connectionStr)
        {
        }
        public DbSet<TaskDB> Tasks { get; set; }
        public DbSet<LinkDB> Links { get; set; }
        public DbSet<EmployeeDB> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDB>()
                .Property(f => f.HiredDate)
                .HasColumnType("datetime2")
                .HasPrecision(0);

            base.OnModelCreating(modelBuilder);
        }
    }
}
