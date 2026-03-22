using MediatR;
using SolTestBackend.Core.Results;

namespace SolTestBackend.Application.Currencies.Queries.GetAll
{
    public record GetAllCurrenciesQuery() : IRequest<Result<IEnumerable<CurrencyDTO>>>;
}
