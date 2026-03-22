using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolTestBackend.Core.Abstractions
{
    public abstract class AggregateRoot : Entity
    {
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(Guid id) : base(id)
        {
        }
    }
}
