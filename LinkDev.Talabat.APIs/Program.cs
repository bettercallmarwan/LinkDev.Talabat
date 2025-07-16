using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Linq;
using System;
using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.APIs.Services;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure services

            // Add services to the container. (DI)

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

   

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
            #endregion

            var webApplication = webApplicationBuilder.Build();


            #region Apply Pending Migrations and Data Seeding
            // Asking runtime env for an object of StoreContext service explicitly
            await webApplication.InitializeStoreContextAsync();
            #endregion

            #region Configure kestrel middlewares

            // Configure the HTTP request pipeline.

            if (webApplication.Environment.IsDevelopment())
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI();
            }

            webApplication.UseHttpsRedirection(); // direct ant http to be https


            webApplication.MapControllers(); 
            #endregion

            webApplication.Run();
        }
    }
}
