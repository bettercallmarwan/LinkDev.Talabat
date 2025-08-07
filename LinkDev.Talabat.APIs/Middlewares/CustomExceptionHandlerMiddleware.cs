using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Controllers.Exceptions;
using System.Net;

namespace LinkDev.Talabat.APIs.Middlewares
{
    //Convension-Based
    public class CustomExceptionHandlerMiddleware 
    {
        // RequestDelegate is a delegate that points to a method that takes (HttpContext)
        private readonly RequestDelegate _next; // points to the next middleware in the pipeline
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger; // for logging errors 
        private readonly IWebHostEnvironment _environment; // to determine if we're in development or production mode

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        // called for every http request
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                // logic executed with request

                await _next(httpContext); // to proceed to the next middleware

                // logic executed with response

            }
            catch (Exception ex)
            {
                ApiResponse response;

                switch (ex)
                {
                    case NotFoundException:
                        httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        httpContext.Response.ContentType = "application/json";

                        response = new ApiResponse(404, ex.Message);

                        await httpContext.Response.WriteAsync(response.ToString());

                        break;
                    default:
                        // Development mode
                        if (_environment.IsDevelopment())
                        {
                            _logger.LogError(ex, ex.Message);
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace?.ToString());
                        }
                        else
                        {
                            // Production mode
                            // Log exception details in db or file
                            response = new ApiExceptionResponse((int)HttpStatusCode.InternalServerError);
                        }

                        // in the header of the response
                        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        httpContext.Response.ContentType = "application/json";

                        // write response
                        await httpContext.Response.WriteAsync(response.ToString());
                        break;
                }
            }
        }
    }
}
