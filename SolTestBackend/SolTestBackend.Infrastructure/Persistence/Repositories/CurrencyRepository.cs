using Microsoft.EntityFrameworkCore;
using SolTestBackend.Domain.Currencies.Entities;
using SolTestBackend.Domain.Currencies.Repositories;
using SolTestBackend.Infrastructure.Persistence.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Infrastructure.Persistence.Repositories
{
    internal class CurrencyRepository : ICurrencyRepository
    {
        private readonly DomainDbContext _dbContext;

        public CurrencyRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Currency currency, CancellationToken ct = default)
        {
            await _dbContext.Currencies.AddAsync(currency, ct);
        }

        public async Task<bool> ExistSymbol(string symbol, CancellationToken ct = default)
        {
            return await _dbContext.Currencies
                .AnyAsync(c => c.Symbol == symbol, ct);
        }

        public async Task<Currency?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var currency = await _dbContext.Currencies
                .FirstOrDefaultAsync(c => c.Id == id, ct);
            return currency;
        }
    }
}
