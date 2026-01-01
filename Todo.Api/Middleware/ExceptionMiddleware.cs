using System.Net;
using System.Text.Json;
using Todo.APi.DTO;
using Todo.APi.Exceptions;

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

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode  = (int)HttpStatusCode.InternalServerError;

            if (exception is ExceptionBase exceptionBase)
            {
                httpContext.Response.StatusCode = (int)exceptionBase.StatusCode;
            }
            else
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var errorResponse = new ErrorResponse
            {
                StatusCode = httpContext.Response.StatusCode,
                Message    = exception.Message,
                Details    = _hostEnvironment.IsDevelopment() ? exception.StackTrace?.ToString() : null
            };

            var jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json                  = JsonSerializer.Serialize(errorResponse, jsonSerializerOptions);

            await httpContext.Response.WriteAsync(json);
        }
    }
}
