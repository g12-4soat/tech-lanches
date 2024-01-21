using Polly;
using Polly.Extensions.Http;
using System.Net.Http.Headers;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Adapter.FilaPedidos;
using TechLanches.Adapter.FilaPedidos.Health;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Adapter.RabbitMq.Options;
using TechLanches.Adapter.SqlServer;
using TechLanches.Adapter.SqlServer.Repositories;
using TechLanches.Application.Controllers;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.Presenters;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Services;
using TechLanches.Domain.Validations;

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
        services.Configure<RabbitOptions>(settingsConfig.GetSection("RabbitMQ"));

        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                  .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(retryAttempt));

        //Registrar httpclient
        services.AddHttpClient("MercadoPago", httpClient =>
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", settingsConfig.GetSection($"ApiMercadoPago:AccessToken").Value);
            httpClient.BaseAddress = new Uri(settingsConfig.GetSection($"ApiMercadoPago:BaseUrl").Value);
        }).AddPolicyHandler(retryPolicy);

        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoCriadoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoCanceladoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoEmPreparacaoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoDescartadoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoFinalizadoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoProntoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoRecebidoValidacao>();
        services.AddSingleton<IStatusPedidoValidacao, StatusPedidoRetiradoValidacao>();

        services.AddSingleton<IPedidoRepository, PedidoRepository>();
        services.AddSingleton<IFilaPedidoRepository, FilaPedidoRepository>();
        services.AddSingleton<IFilaPedidoService, FilaPedidoService>();

        services.AddSingleton<IProdutoPresenter, ProdutoPresenter>();

        services.AddScoped<IPedidoController, PedidoController>();

        services.AddScoped<IPedidoGateway, PedidoGateway>();

        services.AddScoped<IPedidoPresenter, PedidoPresenter>();

        services.AddSingleton<IClienteService, ClienteService>();
        services.AddSingleton<IPagamentoService, PagamentoService>();
        services.AddSingleton<ICheckoutService, CheckoutService>();
        services.AddSingleton<IQrCodeGeneratorService, QrCodeGeneratorService>();
        services.AddSingleton<IPagamentoACLService, MercadoPagoMockadoService>();
        services.AddSingleton<IPagamentoACLService, MercadoPagoService>();
        services.AddSingleton<IStatusPedidoValidacaoService, StatusPedidoValidacaoService>();
        services.AddSingleton<IClienteRepository, ClienteRepository>();
        services.AddSingleton<IProdutoRepository, ProdutoRepository>();
        services.AddSingleton<IPagamentoRepository, PagamentoRepository>();
        services.AddSingleton<IRabbitMqService, RabbitMqService>();
        services.AddHostedService<FilaPedidosHostedService>();

        services.AddHealthChecks()
        .AddCheck<DbHealthCheck>("db_hc")
        .AddCheck<RabbitMQHealthCheck>("rabbit_hc");

        services.AddHostedService<TcpHealthProbeService>();
    });


var host = hostBuilder.Build();

await host.RunAsync();