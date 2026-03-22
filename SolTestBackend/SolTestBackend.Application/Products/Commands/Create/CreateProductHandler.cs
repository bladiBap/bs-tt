using MediatR;
using SolTestBackend.Application.Currencies.Commands.Create;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Core.Interfaces;
using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Currencies.Entities;
using SolTestBackend.Domain.Currencies.Errors;
using SolTestBackend.Domain.Currencies.Repositories;
using SolTestBackend.Domain.Products.Entities;
using SolTestBackend.Domain.Products.Errors;
using SolTestBackend.Domain.Products.Repositories;
using SolTestBackend.Domain.Products.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Application.Products.Commands.Create
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result<ProductDTO>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(
            ICurrencyRepository currencyRepository,
            IProductRepository productRepository,
            IUnitOfWork unitOfWork
        )
        {
            _currencyRepository = currencyRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDTO>> Handle(CreateProductCommand request, CancellationToken ct)
        {
            var currency = await _currencyRepository.GetByIdAsync(request.currencyId, ct);

            if (currency == null)
            {
                return Result.Failure<ProductDTO>(ProductError.CurrencyDontExists);
            }

            var price = PriceVO.Create(request.price);
            var name = request.name;
            var currencyId = request.currencyId;

            var product = Product.Create(
                name,
                price,
                currencyId,
                100
            );

            await _productRepository.AddAsync(product, ct);
            await _unitOfWork.CommitAsync(ct);

            var dto = new ProductDTO(
                product.Id,
                new CurrencyDTO(
                    currency.Id,
                    currency.Symbol
                ),
                product.Stock,
                product.Name,
                product.Price.Value,
                product.Sku
            );

            return Result.Success(dto);
        }
    }
}
