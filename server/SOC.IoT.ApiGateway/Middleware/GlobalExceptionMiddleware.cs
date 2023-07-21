using Newtonsoft.Json;
using System.Net;
using System.Runtime.CompilerServices;

namespace SOC.IoT.ApiGateway.Middleware
{

    public class GlobalExceptionMiddleware
    {
        private readonly ILogger<GlobalExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        private sealed class ErrorResponse
        {
            public string Message { get; set; }
            public string Exception { get; set; }
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError("An error occured during the request. Error: {@err}", exception);

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponse
            {
                Message = "An error occured during the request.",
                Exception = exception.Message
            };

            var json = JsonConvert.SerializeObject(
                errorResponse,
                new JsonSerializerSettings
                {
                    Formatting = Newtonsoft.Json.Formatting.Indented,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }
            );

            _logger.LogError(
                "An error occured during the request. Error: {@err} Exception: {@ex}",
                json,
                exception
            );


            await context.Response.WriteAsync(json);
        }
    }
}
