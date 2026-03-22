using MediatR;
using SolTestBackend.Application.Products.DTO;
using SolTestBackend.Core.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Application.Products.Queries.GetById
{
    public record GetByIdProductQuery
    (
        Guid Id
    ) : IRequest<Result<ProductDTO>>;
}
