using MediatR;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Core.Interfaces;
using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Products.Errors;
using SolTestBackend.Domain.Products.Repositories;
using SolTestBackend.Domain.Products.ValueObjects;

namespace SolTestBackend.Application.Products.Commands.UpdatePrice
{
    public class UpdatePriceProductHandler : IRequestHandler<UpdatePriceProductCommand, Result<ProductDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePriceProductHandler(
            IProductRepository productRepository,
            IUnitOfWork unitOfWork
        )
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<ProductDTO>> Handle(UpdatePriceProductCommand request, CancellationToken ct)
        {
            var product = await _productRepository.GetByIdAsync(request.id, ct);
            
            if (product == null)
            {
                return Result.Failure<ProductDTO>(ProductError.NotFound);
            }

            var newPrice = PriceVO.Create(request.price);
            product.SetPrice(newPrice);

            await _productRepository.UpdateAsync(product, ct);
            await _unitOfWork.CommitAsync(ct);

            var dto = new ProductDTO(
                product.Id,
                null,
                product.Stock,
                product.Name,
                product.Price.Value,
                product.Sku
            );

            return Result.Success(dto);
        }
    }
}
