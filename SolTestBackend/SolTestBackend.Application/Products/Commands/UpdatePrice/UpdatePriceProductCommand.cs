using MediatR;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Core.Results;


namespace SolTestBackend.Application.Products.Commands.UpdatePrice
{
    public record UpdatePriceProductCommand
    (
        Guid id,
        decimal price
    ) : IRequest<Result<ProductDTO>>;
}
