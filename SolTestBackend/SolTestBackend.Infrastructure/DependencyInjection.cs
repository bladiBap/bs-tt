using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SolTestBackend.Core.Interfaces;
using SolTestBackend.Domain.Currencies.Repositories;
using SolTestBackend.Domain.Products.Repositories;
using SolTestBackend.Domain.Users.Repositories;
using SolTestBackend.Infrastructure.Persistence;
using SolTestBackend.Infrastructure.Persistence.DomainModel;
using SolTestBackend.Infrastructure.Persistence.PersistenceModel;
using SolTestBackend.Infrastructure.Persistence.Repositories;
using SolTestBackend.Infrastructure.Services;
using System.Reflection;

namespace SolTestBackend.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            string? connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<PersistenceDbContext>(context =>
                context.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(PersistenceDbContext).Assembly.FullName)));

            services.AddDbContext<DomainDbContext>(options =>
                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly(typeof(DomainDbContext).Assembly.FullName)));

            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AddScoped<IPasswordHasher, PasswordService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
