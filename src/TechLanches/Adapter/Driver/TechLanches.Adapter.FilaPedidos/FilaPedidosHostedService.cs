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

                await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoEmPreparacao);

                await Task.Delay(1000 * 20);

                await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoPronto);

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}