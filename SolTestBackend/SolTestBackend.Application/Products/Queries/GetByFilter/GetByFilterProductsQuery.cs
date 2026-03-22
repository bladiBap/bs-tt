using MediatR;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Core.Results;

namespace SolTestBackend.Application.Products.Queries.GetByFilterProcucts
{
    public record GetByFilterProductsQuery
    (
        int Page,
        int PageSize,
        string? SearchText,
        decimal? MinPrice,
        decimal? MaxPrice,
        Guid? CurrencyId,
        string? SortBy,
        string? SortOrder
    ) : IRequest<Result<ResponsePaged<ProductDTO>>>;
}
