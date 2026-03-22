using SolTestBackend.Core.Results;

namespace SolTestBackend.Domain.Products.Errors
{
    public static class ProductError
    {
        public static Error NotFound =>
            Error.NotFound(
                "Product.NotFound",
                "The specified product was not found."
            );
        public static Error NameIsRequired =>
            Error.Validation(
                "Product.Name.Required",
                "Product name is required"
            );
        public static Error NameInvalidLength(int min, int max) =>
            Error.Validation(
                "Product.Name.Length",
                $"Product name must be between {min} and {max} characters."
            );
        public static Error StockCannotBeLessThanZero =>
            Error.Validation(
                code: "Product.Stock.LessThanZero",
                message: "The Stock cannot be less than zero."
            );
        public static Error CurrencyDontExists =>
            Error.NotFound(
                code: "Product.Currency.NotFound",
                message: "The specified currency does not exist."
            );
    }
}
