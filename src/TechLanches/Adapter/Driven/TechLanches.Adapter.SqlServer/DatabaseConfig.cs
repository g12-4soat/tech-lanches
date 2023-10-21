using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TechLanches.Adapter.SqlServer
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(
            this IServiceCollection services,
            IConfiguration configuration,
            ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<TechLanchesDbContext>(config => 
            {
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }, 
            serviceLifetime);
        }

        public static void UseDatabaseConfiguration(this IApplicationBuilder app)
        {
            if (app is null) throw new ArgumentNullException(nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<TechLanchesDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }

            DataSeeder.Seed(context);
        }

        public static void UseDatabaseConfiguration(this IServiceProvider serviceProvider)
        {
            if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

            using var scope = serviceProvider.CreateScope();
            
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
