using SolTestBackend.Domain.Currencies.Entities;

namespace SolTestBackend.Domain.Currencies.Repositories
{
    public interface ICurrencyRepository
    {
         Task AddAsync(Currency currency, CancellationToken ct = default);
         Task<bool> ExistSymbol(string symbol, CancellationToken ct = default);
         Task<Currency?> GetByIdAsync(Guid id, CancellationToken ct = default);
    }
}
