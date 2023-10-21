using TechLanches.Adapter.SqlServer.Middlewares;

namespace TechLanches.API.Configuration
{
    public static class ApplicationBuilderConfig
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
        => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }
}
