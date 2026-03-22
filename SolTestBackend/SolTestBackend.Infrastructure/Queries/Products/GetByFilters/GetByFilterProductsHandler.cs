using MediatR;
using Microsoft.EntityFrameworkCore;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Application.Products.Queries.GetByFilterProcucts;
using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Currencies.Entities;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel.Models;

namespace SolTestBackend.Infrastructure.Queries.Products.GetByFiltersProducts
{
    internal class GetByFilterProductsHandler :
        IRequestHandler<GetByFilterProductsQuery, Result<ResponsePaged<ProductDTO>>>
    {
        private readonly PersistenceDbContext _dbContext;

        public GetByFilterProductsHandler(PersistenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ResponsePaged<ProductDTO>>> Handle(GetByFilterProductsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<ProductPersistence> query = _dbContext.Products
            .Include(p => p.Currency)
            .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchText))
            {
                var pattern = $"%{request.SearchText.ToLower()}%";

                query = query.Where(p =>
                    EF.Functions.Like(p.Name.ToLower(), pattern) ||
                    EF.Functions.Like(p.Sku.ToLower(), pattern));     
            }

            if (request.MinPrice.HasValue)
                query = query.Where(p => p.PriceAmount >= request.MinPrice.Value);

            if (request.MaxPrice.HasValue)
                query = query.Where(p => p.PriceAmount <= request.MaxPrice.Value);

            if (request.CurrencyId.HasValue && request.CurrencyId != Guid.Empty)
                query = query.Where(p => p.CurrencyId == request.CurrencyId.Value);

            query = request.SortBy?.ToLower() switch
            {
                "price" => request.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(p => p.PriceAmount)
                    : query.OrderBy(p => p.PriceAmount),
                "stock" => request.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(p => p.Stock)
                    : query.OrderBy(p => p.Stock),
                _ => request.SortOrder?.ToLower() == "desc"
                    ? query.OrderByDescending(p => p.Name)
                    : query.OrderBy(p => p.Name),
            };

            var totalItems = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDTO
                (
                    p.Id,
                    new CurrencyDTO(
                        p.Currency.Id,
                        p.Currency.Symbol
                    ),
                    p.Stock,
                    p.Name,
                    p.PriceAmount,
                    p.Sku
                ))
                .ToListAsync(cancellationToken);

            var response = new ResponsePaged<ProductDTO>(
                items,
                totalItems,
                request.Page,
                request.PageSize
            );

            return Result.Success(response);
        }
    }
}
