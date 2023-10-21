using Microsoft.EntityFrameworkCore;
using TechLanches.Adapter.FilaPedidos;
using TechLanches.Adapter.SqlServer;
using TechLanches.Adapter.SqlServer.Repositories;
using TechLanches.Application;
using TechLanches.Application.Ports.Services;
using TechLanches.Domain.Repositories;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var settingsConfig = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        services.AddDbContext<TechLanchesDbContext>(config =>
        {
            config.UseSqlServer(settingsConfig.GetConnectionString("DefaultConnection"));
        }, 
        ServiceLifetime.Singleton);

        services.AddSingleton<IPedidoRepository, PedidoRepository>();
        services.AddSingleton<IFilaPedidoService, FilaPedidoService>();
        services.AddHostedService<FilaPedidosHostedService>();
    });


var host = hostBuilder.Build();

await host.RunAsync();