using SolTestBackend.Application.Currencies.Queries.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Application.Products.DTO
{
    public record ProductDTO(
        Guid id,
        CurrencyDTO? currency,
        int stock,
        string name,
        decimal price,
        string sku
    );
}
