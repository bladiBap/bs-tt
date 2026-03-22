using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace SolTestBackend.Api.Contracts.V1.Product
{
    public record GetByFillterContract
    {
        [FromQuery(Name = "page")]
        public int Page { get; init; } = 1;

        [FromQuery(Name = "pageSize")]
        public int PageSize { get; init; } = 10;

        [FromQuery(Name = "searchText")]
        public string? SearchText{ get; init; }

        [FromQuery(Name = "minPrice")]
        public decimal? MinPrice { get; init; }

        [FromQuery(Name = "maxPrice")]
        public decimal? MaxPrice { get; init; }

        [FromQuery(Name = "currencyId")]
        public Guid? CurrencyId { get; init; }

        [FromQuery(Name = "sortBy")]
        public string? SortBy { get; init; } = "name";

        [FromQuery(Name = "sortOrder")]
        public string? SortOrder { get; init; } = "asc";
    }
}
