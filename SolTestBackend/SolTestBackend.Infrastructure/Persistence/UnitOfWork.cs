using MediatR;
using SolTestBackend.Core.Interfaces;
using SolTestBackend.Infrastructure.Persistence.DomainModel;

namespace SolTestBackend.Infrastructure.Persistence
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly DomainDbContext _dbContext;
        private readonly IMediator _mediator;

        public UnitOfWork(DomainDbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext;
            _mediator = mediator;
        }

        public async Task CommitAsync(CancellationToken ct = default)
        {
            await _dbContext.SaveChangesAsync(ct);
        }
    }
}
