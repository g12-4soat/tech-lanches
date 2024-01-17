using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.FilaPedidos
{
    public class FilaPedidosHostedService : BackgroundService
    {
        private readonly IFilaPedidoService _filaPedidoService;
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<FilaPedidosHostedService> _logger;
        private readonly WorkerOptions _workerOptions;
        private readonly IRabbitMqService _rabbitMqService;
        public FilaPedidosHostedService(
            ILogger<FilaPedidosHostedService> logger,
            IFilaPedidoService filaPedidoService,
            IOptions<WorkerOptions> workerOptions,
            IPedidoService pedidoService,
            IRabbitMqService rabbitMqService)
        {
            _logger = logger;
            _filaPedidoService = filaPedidoService;
            _workerOptions = workerOptions.Value;
            _pedidoService = pedidoService;
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

            var pedido = await _pedidoService.BuscarPorId(pedidoId);

            _logger.LogInformation("Próximo pedido da fila: {proximoPedido.Id}", pedido.Id);

            if (pedido.StatusPedido != StatusPedido.PedidoEmPreparacao)
                await _filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoEmPreparacao);

            _logger.LogInformation("Pedido {proximoPedido.Id} em preparação.", pedido.Id);

            await Task.Delay(1000 * _workerOptions.DelayPreparacaoPedidoEmSegundos);

            _logger.LogInformation("Pedido {proximoPedido.Id} preparação finalizada.", pedido.Id);

            await _filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoPronto);

            _logger.LogInformation("Pedido {proximoPedido.Id} pronto.", pedido.Id);
        }
    }
}