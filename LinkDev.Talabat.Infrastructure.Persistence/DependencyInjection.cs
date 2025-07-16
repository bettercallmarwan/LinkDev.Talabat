using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {

        // Extension method
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //1. Registers StoreContext as a scoped service in the DI container
            //2. Configures it to use SQL Server with connection string from appsettings.json
            services.AddDbContext<StoreContext>((optionBuilder) =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            services.AddScoped<IStoreContextInitializer, StoreContextInitializer>();

            services.AddScoped(typeof(ISaveChangesInterceptor), typeof(CutsomSaveChangesInterceptor));
            return services;
        }
    }
}
