using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SolTestBackend.Api.Contracts.V1.Currency;
using SolTestBackend.Application.Currencies.Commands.Create;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Core.Results;

namespace SolTestBackend.Api.Controllers.V1
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/currencies")]
    public class CurrencyController : BaseController
    {
        private readonly IMediator _mediator;

        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var query = new GetAllCurrenciesQuery();
            Result<IEnumerable<CurrencyDTO>> result = await _mediator.Send(query);
            return HandlerResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyContract contract)
        {
            var command = new CreateCurrencyCommand(contract.Symbol);
            Result<CurrencyDTO> result = await _mediator.Send(command);
            return HandlerResult(result);
        }
    }
}
