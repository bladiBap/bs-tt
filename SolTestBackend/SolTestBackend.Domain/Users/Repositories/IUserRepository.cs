using SolTestBackend.Domain.Users.Entities;
using SolTestBackend.Domain.Users.ValueObjects;

namespace SolTestBackend.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user, CancellationToken ct = default);
        Task<User?> GetByEmailAsync(EmailVO email, CancellationToken ct = default);
    }
}
