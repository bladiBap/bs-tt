using SolTestBackend.Core.Results;
using SolTestBackend.Core.Abstractions;

using SolTestBackend.Domain.Products.Errors;
using SolTestBackend.Domain.Products.ValueObjects;

namespace SolTestBackend.Domain.Products.Entities
{
    public class Product : AggregateRoot
    {
        public string Sku { get; private set; }
        public string Name { get; private set; }
        public PriceVO Price { get; private set; }
        public Guid CurrencyId { get; private set; }
        public int Stock { get; private set; }

        public const int MaxNameLength = 100;
        public const int MinNameLength = 3;

        private Product() { }

        private Product(string name, PriceVO price, Guid currencyId, int stock)
            :base(Guid.NewGuid()) {
            Sku = CreateSku(name);
            Name = name;
            Price = price;
            Stock = stock;
            CurrencyId = currencyId;
        }

        public static Product Create(string name, PriceVO price, Guid currencyId, int stock)
        {
            ValidateName(name);
            ValidateStock(stock);
            return new Product(name, price, currencyId, stock);
        }

        public void SetName(string name)
        {
            ValidateName(name);
            Name = name;
            MarkAsUpdated();
        }

        public void SetPrice(PriceVO price)
        {
            Price = price;
            MarkAsUpdated();
        }

        public void SetStock(int stock)
        {
            ValidateStock(stock);
            Stock = stock;
            MarkAsUpdated();
        }

        public void SetSku(string sku)
        {
            Sku = sku;
            MarkAsUpdated();
        }

        public void SetCurrencyId(Guid currencyId)
        {
            CurrencyId = currencyId;
            MarkAsUpdated();
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new DomainException(ProductError.NameIsRequired);
            }
            if (name.Length < MinNameLength || name.Length > MaxNameLength)
            {
                throw new DomainException(ProductError.NameInvalidLength(3, MaxNameLength));
            }
        }

        private static void ValidateStock(int stock)
        {
            if (stock < 0)
            {
                throw new DomainException(ProductError.StockCannotBeLessThanZero);
            }
        }

        public static string CreateSku(string name)
        {
            string prefix = Guid.NewGuid().ToString()[..5].ToUpper();
            string code = Random.Shared.Next(10000, 100000).ToString();
            return $"{name[..2].ToUpper()}-{prefix}-{code}";
        }
    }
}
