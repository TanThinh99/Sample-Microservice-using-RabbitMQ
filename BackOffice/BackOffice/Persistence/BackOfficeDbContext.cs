using BackOffice.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BackOffice.Persistence
{
    public class BackOfficeDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public BackOfficeDbContext(DbContextOptions<BackOfficeDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
