using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Napa.Data
{
    // add-Migration -Context Napa.Data.MenuDbContext -OutputDir Data\Menu
    // remove-Migration -Context Napa.Data.MenuDbContext

    public class MenuDbContext : DbContext
    {
        public DbSet<MenuCategory> Categories { get; set; }
        public DbSet<MenuCategoryPriceKind> CategoriesPriceKind { get; set; }
        public DbSet<MenuItem> Items { get; set; }
        public DbSet<MenuPrice> Prices { get; set; }
        public MenuDbContext(DbContextOptions<MenuDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MenuCategory>(x => x.HasMany(y => y.Items).WithOne(x => x.Category).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<MenuCategory>(x => x.HasMany(y => y.PriceKinds).WithOne(x => x.Category).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<MenuItem>(x => x.HasMany(y => y.Prices).WithOne(y => y.MenuItem).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<MenuCategoryPriceKind>(x => x.HasMany(y => y.Prices).WithOne(y => y.MenuPriceKind).OnDelete(DeleteBehavior.Restrict));
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
