﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Data.Entities;

namespace WebsiteBuilder.Data
{
    public class WebsiteBuilderDbContext : DbContext
    {
        public WebsiteBuilderDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            Database.SetInitializer<WebsiteBuilderDbContext>(null);
        }

        public DbSet<Website> Websites { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties()
              .Where(p => p.Name == "Id")
              .Configure(p => p.IsKey().HasColumnName(p.ClrPropertyInfo.ReflectedType.Name + "Id"));

        }
    }
}
