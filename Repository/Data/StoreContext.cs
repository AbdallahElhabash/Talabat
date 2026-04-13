using Core.Entites;
using Core.Entites.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Dtata
{
    public class StoreContext:DbContext
    {
        public StoreContext(DbContextOptions<StoreContext>Options):base(Options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> Brands { get; set; }
        public DbSet<ProductType> Types { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set;}
        public DbSet<OrderItem> Items { get; set; }
    }
}
