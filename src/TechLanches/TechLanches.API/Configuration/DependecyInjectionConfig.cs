using TechLanches.Application;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
using TechLanches.Infrastructure.Repositories;

namespace TechLanches.API.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if(services is null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IPedidoService, PedidoService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
        }
    }
}
