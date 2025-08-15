using LinkDev.Talabat.Core.Application.Abstraction.Services;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Application.Mapping;
using LinkDev.Talabat.Core.Application.Services;
using LinkDev.Talabat.Core.Application.Services.Basket;
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
            
            // Register BasketService
            services.AddScoped<IBasketService, BasketService>();

            // Register the factory for BasketService
            //services.AddScoped<Func<IBasketService>>(serviceProvider => () => 
            //    serviceProvider.GetRequiredService<IBasketService>());

            services.AddScoped(typeof(Func<IBasketService>), (serviceProvider) =>
            {
                return () => serviceProvider.GetRequiredService<IBasketService>();
            });
            
            services.AddScoped<IServiceManager, ServiceManager>();
            return services;
        }
    }
}
