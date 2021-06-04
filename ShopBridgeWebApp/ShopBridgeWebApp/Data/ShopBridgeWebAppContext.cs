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
        public ShopBridgeWebAppContext (DbContextOptions<ShopBridgeWebAppContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Inventory>().ToTable("TB_Inventory");
        }
        public DbSet<Inventory> Inventory { get; set; }
    }
}
