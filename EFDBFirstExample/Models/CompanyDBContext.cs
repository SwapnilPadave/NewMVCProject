using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
//using EFDBFirstExample.Migrations;
using System.Configuration;

namespace EFDBFirstExample.Models
{
    public class CompanyDBContext:DbContext
    {
        public CompanyDBContext() : base("MyConnectionString")
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<CompanyDBContext, Configuration>());
        }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}