using Microsoft.Extensions.DependencyInjection;
using OwnerService.Domain.Repositories;
using OwnerService.Domain.Services;
using OwnerService.Infrastructure.Data;
using OwnerService.Infrastructure.Repositories;
using OwnerService.Infrastructure.Services;

namespace OwnerService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<OwnerDbContext>();
            services.AddScoped<IOwnerRepository, OwnerRepository>();
            services.AddScoped<IOwnerCodeGenerator, OwnerCodeGenerator>();

            return services;
        }
    }
}
