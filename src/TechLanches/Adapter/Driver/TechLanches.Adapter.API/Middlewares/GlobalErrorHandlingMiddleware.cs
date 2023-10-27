using System.Net;
using System.Text.Json;
using TechLanches.Core;

namespace TechLanches.Adapter.API.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;

            var exceptionType = exception.GetType();

            switch (exceptionType)
            {
                case Type t when t == typeof(DomainException):
                    status = HttpStatusCode.BadRequest;
                    break;

                case Type t when t == typeof(NotImplementedException):
                    status = HttpStatusCode.NotImplemented;
                    break;

                default:
                    status = HttpStatusCode.InternalServerError;
                    break;
            }

            message = exception.Message;

            var exceptionResult = JsonSerializer.Serialize(new { error = message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}