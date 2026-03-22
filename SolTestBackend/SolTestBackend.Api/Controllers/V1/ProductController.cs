using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SolTestBackend.Api.Contracts.V1.Product;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Application.Products.Commands.Create;
using SolTestBackend.Application.Products.Commands.UpdatePrice;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Application.Products.Queries.GetByFilterProcucts;
using SolTestBackend.Application.Products.Queries.GetById;
using SolTestBackend.Core.Results;

namespace SolTestBackend.Api.Controllers.V1
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/products")]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetByFillterContract contract)
        {
            var query = new GetByFilterProductsQuery(
                contract.Page,
                contract.PageSize,
                contract.SearchText,
                contract.MinPrice,
                contract.MaxPrice,
                contract.CurrencyId,
                contract.SortBy,
                contract.SortOrder
            );
            Result<ResponsePaged<ProductDTO>> result = await _mediator.Send(query);
            return HandlerResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductContract contract)
        {
            var command = new CreateProductCommand(
                contract.Name,
                contract.Price,
                contract.CurrencyId
            );
            Result<ProductDTO> result = await _mediator.Send(command);
            return HandlerResult(result);
        }

        [HttpPatch("{id:guid}/price")]
        public async Task<IActionResult> UpdatePrice(
            [FromRoute] Guid id,
            [FromBody] UpdatePriceContract request)
        {
            var command = new UpdatePriceProductCommand(
                id,
                request.Price
             );

            Result<ProductDTO> result = await _mediator.Send(command);
            return HandlerResult(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken ct)
        {
            var query = new GetByIdProductQuery(id);

            Result<ProductDTO> result = await _mediator.Send(query, ct);

            return HandlerResult(result);
        }
    }
}
