using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShopBridgeWebApp.Models;

namespace ShopBridgeWebApp.Data
{
    public class ShopBridgeWebAppContext : DbContext
    {
        public ShopBridgeWebAppContext(DbContextOptions<ShopBridgeWebAppContext> options)
            : base(options)
        {

        }

        public DbSet<Inventory> Inventory { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    //base.OnConfiguring(optionsBuilder);
        //    optionsBuilder.UseSqlServer(@"Data Source=LAPTOP-ON7MTR4N;Initial Catalog=ShopBridge;Integrated Security=True");
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Inventory>().ToTable("TB_Inventory");
        }

        //public DbSet<Inventory> inventory { get; set; }
    }
}
