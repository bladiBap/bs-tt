using SolTestBackend.Core.Results;

namespace SolTestBackend.Domain.Products.Errors
{
    public static class PriceError
    {
        public static Error PriceCannotBeLessThanZero =>
            Error.Validation(
                code: "Price.LessThanZero",
                message: "The Price cannot be less than zero."
            );
    }
}
