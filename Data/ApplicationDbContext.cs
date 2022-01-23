using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using GoDam.Models;

namespace GoDam.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GoDam.Models.Category> Category { get; set; }
        public DbSet<GoDam.Models.Customer> Customer { get; set; }
        public DbSet<GoDam.Models.Product> Product { get; set; }
        public DbSet<GoDam.Models.Sales> Sales { get; set; }
        public DbSet<GoDam.Models.Purchase> Purchase { get; set; }
        public DbSet<GoDam.Models.ProductStock> ProductStock { get; set; }
        public DbSet<GoDam.Models.PurchaseDetails> PurchaseDetails { get; set; }
        public DbSet<GoDam.Models.SalesDetails> SalesDetails { get; set; }
        //public DbSet<GoDam.Models.Roles> Roles { get; set; }

    }
}
