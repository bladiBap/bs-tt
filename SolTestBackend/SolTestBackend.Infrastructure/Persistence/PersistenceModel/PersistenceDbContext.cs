using Microsoft.EntityFrameworkCore;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel.Models;

namespace SolTestBackend.Infrastructure.Persistence.PersistenceModel
{
    internal class PersistenceDbContext : DbContext
    {
        public DbSet<UserPersistence> Users { get; set; }
        public DbSet<ProductPersistence> Products { get; set; }
        public DbSet<CurrencyPersistence> Currencies { get; set; }

        public PersistenceDbContext(DbContextOptions<PersistenceDbContext> options) : base(options)
        {
        }
    }
}
