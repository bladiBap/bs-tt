using SolTestBackend.Core.Results;
using SolTestBackend.Domain.Products.Errors;

namespace SolTestBackend.Domain.Products.ValueObjects
{
    public record PriceVO
    {
        public decimal Value { get; }

        private PriceVO(decimal value)
        {
            Value = value;
        }

        public static PriceVO Create(decimal value) {
            ValidatePrice(value);
            return new PriceVO(value);
        }

        private static void ValidatePrice(decimal value)
        {
            if (value < 0)
            {
                throw new DomainException(PriceError.PriceCannotBeLessThanZero);
            }
        }
    }
}
