using LinkDev.Talabat.APIs.Controllers;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure.Persistence;
using LinkDev.Talabat.Core.Application;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;
using LinkDev.Talabat.APIs.Controllers.Errors;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LinkDev.Talabat.APIs.Middlewares;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {

        public static async Task Main(string[] args)
        {

            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure services

            // Add services to the container. (DI)


            // my code
            webApplicationBuilder.Services
                .AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = false;
                    // InvalidModelStateResponseFactory property expects a function that:
                    // Takes an ActionContext as input
                    // Returns an IActionResult as output
                    options.InvalidModelStateResponseFactory = (actionContext) => // ActionContext is a class that contains information about the current HTTP request and the action (controller method) being executed
                    {

                        var errors = new Dictionary<string, IEnumerable<string>>();

                        foreach(var kvp in actionContext.ModelState)
                        {
                            if (kvp.Value.Errors.Any())
                            {
                                errors[kvp.Key] = kvp.Value.Errors.Select(E => E.ErrorMessage);
                            }
                        }

                        return new BadRequestObjectResult(new ApiValidationErrorResponse()
                        {
                            Errors = errors
                        });
                    };
                })
                .AddApplicationPart(typeof(LinkDev.Talabat.APIs.Controllers.AssemblyInformation).Assembly);



            // ahmed nasr code
            //webApplicationBuilder.Services
            //    .AddControllers()
            //    .ConfigureApiBehaviorOptions(options =>
            //    {
            //        options.SuppressModelStateInvalidFilter = false;
            //        options.InvalidModelStateResponseFactory = (actionContext) =>
            //        {
            //            var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count() > 0)
            //                                .SelectMany(p => p.Value!.Errors)
            //                                .Select(E => E.ErrorMessage);

            //            return new BadRequestObjectResult(new ApiValidationErrorResponse()
            //            {
            //                Errors = errors
            //            });
            //        };
            //    })
            //    .AddApplicationPart(typeof(LinkDev.Talabat.APIs.Controllers.AssemblyInformation).Assembly);


            //webApplicationBuilder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = false;
            //    options.InvalidModelStateResponseFactory = (actionContext) =>
            //    {
            //        var errors = actionContext.ModelState.Where(p => p.Value!.Errors.Count() > 0)
            //                            .SelectMany(p => p.Value!.Errors)
            //                            .Select(E => E.ErrorMessage);

            //        return new BadRequestObjectResult(new ApiValidationErrorResponse()
            //        {
            //            Errors = errors
            //        });
            //    };
            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();
            webApplicationBuilder.Services.AddHttpContextAccessor();
            webApplicationBuilder.Services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));

   

            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
            webApplicationBuilder.Services.AddApplicationServices();
            #endregion

            var webApplication = webApplicationBuilder.Build();


            #region Apply Pending Migrations and Data Seeding
            // Asking runtime env for an object of StoreContext service explicitly
            await webApplication.InitializeStoreContextAsync();
            #endregion

            #region Configure kestrel middlewares

            // Configure the HTTP request pipeline.

            webApplication.UseMiddleware<CustomExceptionHandlerMiddleware>();

            if (webApplication.Environment.IsDevelopment())
            {
                webApplication.UseSwagger();
                webApplication.UseSwaggerUI();
            }

            webApplication.UseHttpsRedirection(); // direct ant http to be https

            webApplication.UseAuthentication();
            webApplication.UseAuthorization();

            webApplication.UseStaticFiles();

            webApplication.MapControllers(); 
            #endregion

            webApplication.Run();
        }
    }
}
