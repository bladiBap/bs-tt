using Microsoft.EntityFrameworkCore;
using SolTestBackend.Domain.Currencies.Entities;
using SolTestBackend.Domain.Products.Entities;
using SolTestBackend.Domain.Users.Entities;
using System.Reflection;

namespace SolTestBackend.Infrastructure.Persistence.DomainModel
{
    internal class DomainDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }

        public DomainDbContext(DbContextOptions<DomainDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}
