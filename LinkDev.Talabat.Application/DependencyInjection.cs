using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());
            
            // Register ProductPictureUrlResolver for AutoMapper to resolve
            services.AddScoped<ProductPictureUrlResolver>();
            
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
