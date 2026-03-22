using SolTestBackend.Domain.Products.Entities;
using SolTestBackend.Domain.Products.ValueObjects;

namespace SolTestBackend.Domain.Products.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken ct = default);
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<bool> ExistAsync(Guid id, CancellationToken ct = default);
    }
}
