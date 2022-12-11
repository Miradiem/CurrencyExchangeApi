using System.Net;
using System.Text.Json;

namespace CurrencyExchangeApi.App
{
    public class ExceptionHandling
    {
        private readonly RequestDelegate _next;

        public ExceptionHandling(RequestDelegate next)
        {
            _next = next;
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

            Logs.Log.Error(exception.Message);
        }
    }
}
