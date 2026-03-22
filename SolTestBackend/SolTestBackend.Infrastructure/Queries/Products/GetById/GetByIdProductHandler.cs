using MediatR;
using Microsoft.EntityFrameworkCore;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Application.Products.Queries.GetByFilterProcucts;
using SolTestBackend.Application.Products.Queries.GetById;
using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Products.Errors;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Infrastructure.Queries.Products.GetById
{
    internal class GetByIdProductHandler : IRequestHandler<GetByIdProductQuery, Result<ProductDTO>>
    {
        private readonly PersistenceDbContext _dbContext;

        public GetByIdProductHandler(PersistenceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<ProductDTO>> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {
            var product = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

            if (product is null)
            {
                return Result.Failure<ProductDTO>(ProductError.NotFound);
            }

            return Result.Success(product);
        }
    }
}
