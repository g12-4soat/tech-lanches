using Microsoft.EntityFrameworkCore;
using TechLanches.Adapter.SqlServer;
using TechLanches.Infrastructure;

namespace TechLanches.API.Configuration
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<TechLanchesDbContext>(config =>
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context = services.GetRequiredService<TechLanchesDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                DataSeeder.Seed(context);
            }
        }
    }
}
