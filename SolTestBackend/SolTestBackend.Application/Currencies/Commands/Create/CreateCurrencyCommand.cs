using MediatR;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Core.Results;
namespace SolTestBackend.Application.Currencies.Commands.Create
{
    public record CreateCurrencyCommand
    (
        string Symbol
    ) : IRequest<Result<CurrencyDTO>>;
}
