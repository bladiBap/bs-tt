using System.Text.Json.Serialization;

namespace SolTestBackend.Api.Contracts.V1.Currency
{
    public record CreateCurrencyContract
    {
        [property: JsonPropertyName("symbol")]
        public required string Symbol { get; init; }
    }
}
