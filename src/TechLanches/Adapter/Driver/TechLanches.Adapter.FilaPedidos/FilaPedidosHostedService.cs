using Microsoft.Extensions.Options;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.FilaPedidos
{
    public class FilaPedidosHostedService : BackgroundService
    {
        private readonly IFilaPedidoService _filaPedidoService;
        private readonly ILogger<FilaPedidosHostedService> _logger;
        private readonly WorkerOptions _workerOptions;

        public FilaPedidosHostedService(
            ILogger<FilaPedidosHostedService> logger,
            IFilaPedidoService filaPedidoService,
            IOptions<WorkerOptions> workerOptions)
        {
            _logger = logger;
            _filaPedidoService = filaPedidoService;
            _workerOptions = workerOptions.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_workerOptions.DelayVerificacaoFilaEmSegundos * 1000, stoppingToken);

                try
                {
                    _logger.LogInformation("FilaPedidosHostedService iniciado: {time}", DateTimeOffset.Now);

                    var proximoPedido = await _filaPedidoService.RetornarPrimeiroPedidoDaFila();

                    if (proximoPedido is not null)
                    {
                        _logger.LogInformation("Próximo pedido da fila: {proximoPedido.Id}", proximoPedido.Id);

                        await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoEmPreparacao);

                        _logger.LogInformation("Pedido {proximoPedido.Id} em preparação.", proximoPedido.Id);

                        await Task.Delay(1000 * _workerOptions.DelayPreparacaoPedidoEmSegundos, stoppingToken);

                        _logger.LogInformation("Pedido {proximoPedido.Id} preparação finalizada.", proximoPedido.Id);

                        await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoPronto);

                        _logger.LogInformation("Pedido {proximoPedido.Id} pronto.", proximoPedido.Id);
                    }
                    else
                    {
                        _logger.LogInformation("Nenhum Pedido na fila. Verificando novamente em 5 segundos.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao processar fila de pedidos.");
                }
            }
        }
    }
}