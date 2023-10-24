using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TechLanches.Adapter.FilaPedidos;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Adapter.SqlServer;
using TechLanches.Adapter.SqlServer.Repositories;
using TechLanches.Application;
using TechLanches.Application.Ports.Repositories;
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

        services.AddDatabaseConfiguration(settingsConfig, ServiceLifetime.Singleton);
        services.Configure<WorkerOptions>(settingsConfig.GetSection("Worker"));

        services.AddSingleton<IPedidoRepository, PedidoRepository>();
        services.AddSingleton<IFilaPedidoRepository, FilaPedidoRepository>();
        services.AddSingleton<IFilaPedidoService, FilaPedidoService>();
        services.AddHostedService<FilaPedidosHostedService>();
    });


var host = hostBuilder.Build();

host.Services.UseDatabaseConfiguration();

await host.RunAsync();