using MediatR;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SolTestBackend.Application.Shared;

namespace SolTestBackend.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(config =>
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
            );

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
