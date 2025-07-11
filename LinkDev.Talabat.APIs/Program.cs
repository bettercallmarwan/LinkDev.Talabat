using LinkDev.Talabat.Infrastructure.Persistence;
namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            #region Configure services

            // Add services to the container. (DI)

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

            //webApplicationBuilder.Services.AddDbContext<StoreContext>((optionBuilder) =>
            //{
            //    optionBuilder.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("StoreContext"));
            //});


            webApplicationBuilder.Services.AddPersistenceServices(webApplicationBuilder.Configuration);
            #endregion

            var webApplication = webApplicationBuilder.Build();

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
