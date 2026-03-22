using System.Text.Json.Serialization;

namespace SolTestBackend.Api.Contracts.V1.Product
{
    public record UpdatePriceContract
    {

        [property: JsonPropertyName("price")]
        public required decimal Price { get; init; }
    }
}
