using MediatR;
using SolTestBackend.Application.Currencies.Queries.GetAll;
using SolTestBackend.Core.Interfaces;
using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Currencies.Entities;
using SolTestBackend.Domain.Currencies.Errors;
using SolTestBackend.Domain.Currencies.Repositories;
using SolTestBackend.Domain.Products.Entities;
using SolTestBackend.Domain.Products.Repositories;

namespace SolTestBackend.Application.Currencies.Commands.Create
{
    public class CreateCurrencyHandler : IRequestHandler<CreateCurrencyCommand, Result<CurrencyDTO>>
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCurrencyHandler(
            ICurrencyRepository currencyRepository,
            IUnitOfWork unitOfWork
        )
        {
            _currencyRepository = currencyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CurrencyDTO>> Handle(CreateCurrencyCommand request, CancellationToken ct)
        {
            var  existSymbol = await _currencyRepository.ExistSymbol(request.Symbol, ct);

            if (existSymbol)
            {
                return Result.Failure<CurrencyDTO>(CurrencyError.SymbolAlreadyExists);
            }

            var currency = Currency.Create(request.Symbol);

            await _currencyRepository.AddAsync(currency, ct);
            await _unitOfWork.CommitAsync(ct);

            var dto = new CurrencyDTO(
                currency.Id,
                currency.Symbol
            );

            return Result.Success(dto);
        }
    }
}
