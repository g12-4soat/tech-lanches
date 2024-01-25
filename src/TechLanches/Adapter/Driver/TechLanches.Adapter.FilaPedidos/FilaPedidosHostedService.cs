using Microsoft.Extensions.Options;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.FilaPedidos
{
    public class FilaPedidosHostedService : BackgroundService
    {
        private readonly IFilaPedidoController _filaPedidoController;
        private readonly IPedidoGateway _pedidoGateway;
        private readonly ILogger<FilaPedidosHostedService> _logger;
        private readonly WorkerOptions _workerOptions;
        private readonly IRabbitMqService _rabbitMqService;
        public FilaPedidosHostedService(
            ILogger<FilaPedidosHostedService> logger,
            IFilaPedidoController filaPedidoService,
            IOptions<WorkerOptions> workerOptions,
            IPedidoRepository pedidoRepository,
            IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _filaPedidoController = filaPedidoService;
            _workerOptions = workerOptions.Value;
            _pedidoGateway = new PedidoGateway(pedidoRepository);
            _rabbitMqService = rabbitMqService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _rabbitMqService.Consumir(ProcessMessageAsync);
            await Task.Delay(Timeout.Infinite, stoppingToken);
        }

        public async Task ProcessMessageAsync(string message)
        {
            var pedidoId = Convert.ToInt32(message);

            _logger.LogInformation("FilaPedidosHostedService iniciado: {time}", DateTimeOffset.Now);

            var pedido = await _pedidoGateway.BuscarPorId(pedidoId);

            _logger.LogInformation("Próximo pedido da fila: {proximoPedido.Id}", pedido.Id);

            if (pedido.StatusPedido != StatusPedido.PedidoEmPreparacao)
                await _filaPedidoController.TrocarStatus(pedido, StatusPedido.PedidoEmPreparacao);

            _logger.LogInformation("Pedido {proximoPedido.Id} em preparação.", pedido.Id);

            await Task.Delay(1000 * _workerOptions.DelayPreparacaoPedidoEmSegundos);

            _logger.LogInformation("Pedido {proximoPedido.Id} preparação finalizada.", pedido.Id);

            await _filaPedidoController.TrocarStatus(pedido, StatusPedido.PedidoPronto);

            _logger.LogInformation("Pedido {proximoPedido.Id} pronto.", pedido.Id);
        }
    }
}