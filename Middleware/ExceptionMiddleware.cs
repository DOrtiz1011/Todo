using System.Net;
using System.Text.Json;
using Todo.APi.DTO;

namespace Todo.APi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate              _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment             _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostEnvironment)
        {
            _requestDelegate = requestDelegate;
            _logger          = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context); // Move to the next piece of middleware (the Controller)
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode  = (int)HttpStatusCode.InternalServerError;

            var response = new ErrorResponse
            {
                StatusCode = context.Response.StatusCode,
                Message    = "An unexpected error occurred on the server.",
                Details    = _hostEnvironment.IsDevelopment() ? ex.StackTrace?.ToString() : null
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json    = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }

}
