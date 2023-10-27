
using TechLanches.Adapter.API.Middlewares;

namespace TechLanches.Adapter.API.Configuration
{
    public static class ApplicationBuilderConfig
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}