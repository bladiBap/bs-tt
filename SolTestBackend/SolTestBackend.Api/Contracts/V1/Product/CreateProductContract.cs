using System.Text.Json.Serialization;

namespace SolTestBackend.Api.Contracts.V1.Product
{
    public record CreateProductContract
    {
        [property: JsonPropertyName("Name")]
        public required string Name { get; init; }

        [property: JsonPropertyName("Price")]
        public required decimal Price { get; init; }

        [property: JsonPropertyName("CurrencyId")]
        public required Guid CurrencyId { get; init; }

    }
}
