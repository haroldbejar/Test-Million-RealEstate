using Microsoft.Extensions.DependencyInjection;
using PropertyService.Application.Services;

namespace PropertyService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IPropertyAppService, PropertyAppService>();

            return services;
        }
    }
}
