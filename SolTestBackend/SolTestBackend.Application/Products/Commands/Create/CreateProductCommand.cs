using MediatR;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Core.Results;

namespace SolTestBackend.Application.Products.Commands.Create
{
    public record CreateProductCommand
    (
        string name,
        decimal price,
        Guid currencyId
    ) : IRequest<Result<ProductDTO>>;
}
