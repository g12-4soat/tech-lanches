using TechLanches.Adapter.FilaPedidos;
using TechLanches.Adapter.FilaPedidos.Health;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Adapter.SqlServer;
using TechLanches.Adapter.SqlServer.Repositories;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Services;

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
        services.AddSingleton<IStatusPedidoValidacaoService, StatusPedidoValidacaoService>();

        services.AddHostedService<FilaPedidosHostedService>();

        services.AddHealthChecks().AddCheck<DbHealthCheck>("db_hc");
        services.AddHostedService<TcpHealthProbeService>();
    });


var host = hostBuilder.Build();

await host.RunAsync();