using MediatR;
using Microsoft.EntityFrameworkCore;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Core.Results;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel;

namespace SolTestBackend.Infrastructure.Queries.Currencies
{
    internal class GetAllCurrenciesHandler : 
        IRequestHandler<GetAllCurrenciesQuery, Result<IEnumerable<CurrencyDTO>>>
    {
        private readonly PersistenceDbContext _dbContext;

        public GetAllCurrenciesHandler(PersistenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<IEnumerable<CurrencyDTO>>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
        {
            var currencies = await _dbContext.Currencies
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            var dtos = currencies.Select(c => new CurrencyDTO(
                c.Id,
                c.Symbol
            ));

            return Result<IEnumerable<CurrencyDTO>>.Success(dtos);
        }
    }
}
