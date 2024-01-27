
using TechLanches.Adapter.API.Middlewares;

namespace TechLanches.Adapter.API.Configuration
{
    public static class ApplicationBuilderConfig
    {
        public static IApplicationBuilder AddCustomMiddlewares(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseMiddleware<RequestLoggingMiddleware>();
            applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();

            return applicationBuilder;
        }
    }
}