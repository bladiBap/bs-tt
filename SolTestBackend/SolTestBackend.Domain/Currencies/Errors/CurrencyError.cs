using SolTestBackend.Core.Results;

namespace SolTestBackend.Domain.Currencies.Errors
{
    public static class CurrencyError
    {
        public static Error IsEmpty =>
            Error.Validation(
                "Currency.Empty",
                "Currency cannot be empt1y."
            );
        public static Error SymbolAlreadyExists =>
            Error.Conflict(
                "Currency.SymbolAlreadyExists",
                "A currency with the same symbol already exists."
            );
    }
}
