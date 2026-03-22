using SolTestBackend.Core.Results;
using SolTestBackend.Core.Abstractions;
using SolTestBackend.Domain.Currencies.Errors;

namespace SolTestBackend.Domain.Currencies.Entities
{
    public class Currency : AggregateRoot
    {

        public string Symbol { get; private set; }

        private Currency()
        {
        }

        private Currency(string symbol) :
            base(Guid.NewGuid())
        {
            Symbol = symbol;
        }

        public static Currency Create(string symbol)
        {
            ValidateSymbol(symbol);
            return new Currency(symbol);
        }

        public void SetSymbol(string symbol)
        {
            ValidateSymbol(symbol);
            Symbol = symbol;
            MarkAsUpdated();
        }

        private static void ValidateSymbol(string symbol)
        {
            if (IsInvalidSymbol(symbol))
            {
                throw new DomainException(CurrencyError.IsEmpty);
            }
        }

        private static bool IsInvalidSymbol(string symbol)
        {
            return string.IsNullOrWhiteSpace(symbol);
        }
    }
}
