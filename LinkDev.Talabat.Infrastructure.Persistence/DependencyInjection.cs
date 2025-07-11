using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace LinkDev.Talabat.Infrastructure.Persistence
{
    public static class DependencyInjection
    {

        // Extension method
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>((optionBuilder) =>
            {
                optionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            return services;
        }
    }
}
