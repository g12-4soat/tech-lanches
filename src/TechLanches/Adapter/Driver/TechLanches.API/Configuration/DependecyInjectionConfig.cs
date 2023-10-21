using TechLanches.Adapter.SqlServer.Repositories;
using TechLanches.Application;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;

namespace TechLanches.API.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IClienteService, ClienteService>();

            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();

            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
