using Microsoft.Extensions.DependencyInjection;
using PropertyService.Domain.Repositories;
using PropertyService.Domain.Services;
using PropertyService.Infrastructure.Data;
using PropertyService.Infrastructure.Repositories;
using PropertyService.Infrastructure.Services;

namespace PropertyService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<PropertyDbContext>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();


            return services;
        }
    }
}