using TechLanches.Application.Ports.Services;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.FilaPedidos
{
    public class FilaPedidosHostedService : BackgroundService
    {
        private readonly IFilaPedidoService _filaPedidoService;
        private readonly ILogger<FilaPedidosHostedService> _logger;

        public FilaPedidosHostedService(
            ILogger<FilaPedidosHostedService> logger,
            IFilaPedidoService filaPedidoService)
        {
            _logger = logger;
            _filaPedidoService = filaPedidoService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("FilaPedidosHostedService iniciado: {time}", DateTimeOffset.Now);

                var proximoPedido = await _filaPedidoService.RetornarPrimeiroPedidoDaFila();

                if (proximoPedido is not null)
                {
                    _logger.LogInformation($"Próximo pedido da fila: {proximoPedido.Id}");

                    await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoEmPreparacao);

                    _logger.LogInformation($"Pedido {proximoPedido.Id} em preparação.");

                    await Task.Delay(1000 * 20);

                    _logger.LogInformation($"Pedido {proximoPedido.Id} preparação finalizada.");

                    await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoPronto);
                    
                    _logger.LogInformation($"Pedido {proximoPedido.Id} pronto.");
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}