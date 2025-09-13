using Microsoft.Extensions.DependencyInjection;
using OwnerService.Application.Services;

namespace OwnerService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IOwnerApplicationService, OwnerApplicationService>();

            return services;
        }
    }
}
