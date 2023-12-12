using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace TechLanches.Adapter.API.Configuration
{
    public static class HealthCheckConfig
    {
        public static void AddHealthCheckConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(connectionString: configuration.GetConnectionString("DefaultConnection"), name: "Banco de dados Tech Lanches");
        }

        public static void AddHealthCheckEndpoint(this IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("/health", new HealthCheckOptions
                {
                    Predicate = _ => true,
                });
            });
        }
    }
}
