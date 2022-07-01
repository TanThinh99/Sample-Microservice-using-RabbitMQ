using Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Inventory.Persistence
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<ProductTransaction> Transactions { get; set; }

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        } 
    }
}
