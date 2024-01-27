using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Adapter.SqlServer.Repositories;
using TechLanches.Application.Controllers;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.Presenters;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Services;
using TechLanches.Domain.Validations;

namespace TechLanches.Adapter.API.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services is null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<IStatusPedidoValidacao, StatusPedidoCriadoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoCanceladoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoCanceladoPorPagamentoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoEmPreparacaoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoDescartadoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoFinalizadoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoProntoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoRecebidoValidacao>();
            services.AddScoped<IStatusPedidoValidacao, StatusPedidoRetiradoValidacao>();

            services.AddSingleton<IProdutoPresenter, ProdutoPresenter>();
            services.AddSingleton<IPedidoPresenter, PedidoPresenter>();
            services.AddSingleton<IPagamentoPresenter, PagamentoPresenter>();
            services.AddSingleton<ICheckoutPresenter, CheckoutPresenter>();
            services.AddSingleton<IClientePresenter, ClientePresenter>();

            services.AddScoped<IProdutoController, ProdutoController>();
            services.AddScoped<IPedidoController, PedidoController>();
            services.AddScoped<IPagamentoController, PagamentoController>();
            services.AddScoped<ICheckoutController, CheckoutController>();            
            services.AddScoped<IClienteController, ClienteController>();

            services.AddScoped<IQrCodeGeneratorService, QrCodeGeneratorService>();
            services.AddScoped<IMercadoPagoMockadoService, MercadoPagoMockadoService>();
            services.AddScoped<IMercadoPagoService, MercadoPagoService>();
            services.AddScoped<IStatusPedidoValidacaoService, StatusPedidoValidacaoService>();

            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IPagamentoRepository, PagamentoRepository>();

            services.AddSingleton<IRabbitMqService, RabbitMqService>();            
        }
    }
}