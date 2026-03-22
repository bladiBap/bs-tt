using FluentValidation;

namespace SolTestBackend.Application.Currencies.Commands.Create
{
    public class CreateCurrencyValidator : AbstractValidator<CreateCurrencyCommand>
    {
        public CreateCurrencyValidator()
        {
            RuleFor(x => x.Symbol)
                .NotEmpty()
                .MinimumLength(2).WithMessage("The symbol must be at least 2 characters long.")
                .MaximumLength(5).WithMessage("The symbol must be no more than 5 characters long.");
        }
    }
}
