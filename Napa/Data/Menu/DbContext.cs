using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Napa.Data.Menu
{
    // add-Migration -Context Napa.Data.Menu.DbContext -OutputDir Data\Menu\Migrations
    // remove-Migration -Context Napa.Data.Menu.DbContext

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryPriceKind> CategoriesPriceKind { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Price> Prices { get; set; }
        public DbContext(DbContextOptions<DbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(x => x.HasMany(y => y.Items).WithOne(x => x.Category).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<Category>(x => x.HasMany(y => y.PriceKinds).WithOne(x => x.Category).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<Item>(x => x.HasMany(y => y.Prices).WithOne(y => y.MenuItem).OnDelete(DeleteBehavior.Restrict));
            modelBuilder.Entity<CategoryPriceKind>(x => x.HasMany(y => y.Prices).WithOne(y => y.MenuPriceKind).OnDelete(DeleteBehavior.Restrict));
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
