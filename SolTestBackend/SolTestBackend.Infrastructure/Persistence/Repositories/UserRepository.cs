using Microsoft.EntityFrameworkCore;
using SolTestBackend.Domain.Users.Entities;
using SolTestBackend.Domain.Users.Repositories;
using SolTestBackend.Domain.Users.ValueObjects;
using SolTestBackend.Infrastructure.Persistence.DomainModel;

namespace SolTestBackend.Infrastructure.Persistence.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly DomainDbContext _dbContext;

        public UserRepository(DomainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(User user, CancellationToken ct = default)
        {
            await _dbContext.Users.AddAsync(user, ct);
        }

        public async Task<User?> GetByEmailAsync(EmailVO email, CancellationToken ct = default)
        {
            User? user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Email == email.Email, ct);
            return user;
        }
    }
}
