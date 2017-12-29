using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public DbSet<WebsiteText> WebsiteTexts { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<WebsiteImage> WebsiteImages { get; set; }
        public DbSet<WebsiteNavigationBar> WebsiteNavigationBar { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Properties()
              .Where(p => p.Name == "Id")
              .Configure(p => p.IsKey().HasColumnName(p.ClrPropertyInfo.ReflectedType.Name + "Id"));

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
