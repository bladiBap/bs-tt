using Microsoft.EntityFrameworkCore;
using SolTestBackend.Domain.Products.Entities;
using SolTestBackend.Domain.Products.Repositories;
using SolTestBackend.Domain.Products.ValueObjects;
using SolTestBackend.Infrastructure.Persistence.DomainModel;

namespace SolTestBackend.Infrastructure.Persistence.Repositories
{
    internal class ProductRepository : IProductRepository
    {
        private readonly DomainDbContext _dbContext;

        public ProductRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Product product, CancellationToken ct = default)
        {
            await _dbContext.Products.AddAsync(product, ct);
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            Product? product = await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, ct);
            return product;
        }

        public Task UpdateAsync(Product product, CancellationToken ct = default)
        {
            _dbContext.Products.Update(product);
            return Task.CompletedTask;
        }

        public async Task<bool> ExistAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == id, ct);
        }
    }
}
