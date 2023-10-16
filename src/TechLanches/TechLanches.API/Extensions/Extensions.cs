using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using TechLanches.Infrastructure;
using System.Data.Common;
using TechLanches.Application;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
using TechLanches.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;
using Mapster;
using System.Reflection;
using MapsterMapper;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.API.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TechLanchesDbContext>(config =>
                config.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services)
        {
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();

            return services;
        }
        public static IServiceCollection AddIntegrationRepository(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();

            return services;
        }

        public static void RegisterMaps(this IServiceCollection services)
        {
            TypeAdapterConfig<Produto, ProdutoResponseDTO>.NewConfig()
                .Map(dest => dest.Categoria, src => CategoriaProduto.From(src.Categoria.Id));

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
