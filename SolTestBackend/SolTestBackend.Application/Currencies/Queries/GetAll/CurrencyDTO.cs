using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Application.Currencies.Queries.GetAll
{
    public record CurrencyDTO(
        Guid id,
        string symbol
    );
}
