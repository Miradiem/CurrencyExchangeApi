using System.Net;
using System.Text.Json;

namespace CurrencyExchangeApi.App
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandling> _logger;

        public ExceptionHandling(RequestDelegate next, ILogger<ExceptionHandling> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }
        private async Task HandleException(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;
            
            var responseMessage = "Something unexpected just happened. Please contact the administrator.";
            var result = JsonSerializer.Serialize(responseMessage);
            await context.Response.WriteAsync(result);

           _logger.LogError(exception.Message);
        }
    }
}
