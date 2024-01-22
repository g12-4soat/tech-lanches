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
using Polly;
using Polly.Extensions.Http;
using TechLanches.Application.Options;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var settingsConfig = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}.json", true, true)
            .AddEnvironmentVariables()
            .Build();


        services.AddDatabaseConfiguration(settingsConfig, ServiceLifetime.Singleton);
        services.Configure<WorkerOptions>(settingsConfig.GetSection("Worker"));
        services.Configure<RabbitOptions>(settingsConfig.GetSection("RabbitMQ"));
        services.Configure<ApplicationOptions>(settingsConfig.GetSection("ApiMercadoPago"));

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

        services.AddSingleton<IProdutoPresenter, ProdutoPresenter>();
        services.AddSingleton<IPedidoPresenter, PedidoPresenter>();
        services.AddSingleton<IPagamentoPresenter, PagamentoPresenter>();
        services.AddSingleton<ICheckoutPresenter, CheckoutPresenter>();
        services.AddSingleton<IClientePresenter, ClientePresenter>();

        services.AddScoped<IPedidoController, PedidoController>();
        services.AddScoped<IFilaPedidoController, FilaPedidoController>();
        services.AddScoped<IPagamentoController, PagamentoController>();
        services.AddScoped<ICheckoutController, CheckoutController>();
        services.AddSingleton<IProdutoController, ProdutoController>();
        services.AddSingleton<IClienteController, ClienteController>();

        services.AddScoped<IPedidoGateway, PedidoGateway>();
        services.AddScoped<IFilaPedidoGateway, FilaPedidoGateway>();
        services.AddScoped<IPagamentoGateway, PagamentoGateway>();
        services.AddSingleton<IProdutoGateway, ProdutoGateway>();
        services.AddSingleton<IClienteGateway, ClienteGateway>();

        services.AddSingleton<IClienteService, ClienteService>();
        services.AddSingleton<IPedidoService, PedidoService>();
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

        services.AddHttpClient("MercadoPago", httpClient =>
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", settingsConfig.GetSection($"ApiMercadoPago:AccessToken").Value);
            httpClient.BaseAddress = new Uri(settingsConfig.GetSection($"ApiMercadoPago:BaseUrl").Value);
        }).AddPolicyHandler(retryPolicy);

        services.AddHealthChecks()
        .AddCheck<DbHealthCheck>("db_hc")
        .AddCheck<RabbitMQHealthCheck>("rabbit_hc");

        services.AddHostedService<TcpHealthProbeService>();
    });


var host = hostBuilder.Build();

await host.RunAsync();