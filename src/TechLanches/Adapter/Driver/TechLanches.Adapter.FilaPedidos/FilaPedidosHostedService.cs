using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using TechLanches.Adapter.FilaPedidos.Options;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.FilaPedidos
{
    public class FilaPedidosHostedService : BackgroundService
    {
        private readonly IFilaPedidoService _filaPedidoService;
        private readonly IPedidoService _pedidoService;
        private readonly ILogger<FilaPedidosHostedService> _logger;
        private readonly WorkerOptions _workerOptions;
        private const string QUEUENAME = "pedidos";

        public FilaPedidosHostedService(
            ILogger<FilaPedidosHostedService> logger,
            IFilaPedidoService filaPedidoService,
            IOptions<WorkerOptions> workerOptions,
            IPedidoService pedidoService)
        {
            _logger = logger;
            _filaPedidoService = filaPedidoService;
            _workerOptions = workerOptions.Value;
            _pedidoService = pedidoService;
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        await Task.Delay(_workerOptions.DelayVerificacaoFilaEmSegundos * 1000, stoppingToken);

        //        try
        //        {
        //            _logger.LogInformation("FilaPedidosHostedService iniciado: {time}", DateTimeOffset.Now);

        //            var proximoPedido = await _filaPedidoService.RetornarPrimeiroPedidoDaFila();

        //            if (proximoPedido is not null)
        //            {
        //                _logger.LogInformation("Próximo pedido da fila: {proximoPedido.Id}", proximoPedido.Id);

        //                if (proximoPedido.StatusPedido != StatusPedido.PedidoEmPreparacao)
        //                    await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoEmPreparacao);

        //                _logger.LogInformation("Pedido {proximoPedido.Id} em preparação.", proximoPedido.Id);

        //                await Task.Delay(1000 * _workerOptions.DelayPreparacaoPedidoEmSegundos, stoppingToken);

        //                _logger.LogInformation("Pedido {proximoPedido.Id} preparação finalizada.", proximoPedido.Id);

        //                await _filaPedidoService.TrocarStatus(proximoPedido, StatusPedido.PedidoPronto);

        //                _logger.LogInformation("Pedido {proximoPedido.Id} pronto.", proximoPedido.Id);
        //            }
        //            else
        //            {
        //                _logger.LogInformation("Nenhum Pedido na fila. Verificando novamente em 5 segundos.");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Erro ao processar fila de pedidos.");
        //        }
        //    }
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var factory = new ConnectionFactory { HostName = "localhost", UserName = "admin", Password = "123456" }; // Configurações de conexão com o RabbitMQ
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: QUEUENAME, durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    _logger.LogInformation("FilaPedidosHostedService iniciado: {time}", DateTimeOffset.Now);

                    var body = ea.Body.ToArray();
                    var pedidoId = Convert.ToInt32(Encoding.UTF8.GetString(body));

                    var pedido = await _pedidoService.BuscarPorId(pedidoId);

                    _logger.LogInformation("Próximo pedido da fila: {proximoPedido.Id}", pedido.Id);

                    if (pedido.StatusPedido != StatusPedido.PedidoEmPreparacao)
                        await _filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoEmPreparacao);

                    _logger.LogInformation("Pedido {proximoPedido.Id} em preparação.", pedido.Id);

                    await Task.Delay(1000 * _workerOptions.DelayPreparacaoPedidoEmSegundos, stoppingToken);

                    _logger.LogInformation("Pedido {proximoPedido.Id} preparação finalizada.", pedido.Id);

                    await _filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoPronto);

                    _logger.LogInformation("Pedido {proximoPedido.Id} pronto.", pedido.Id);
                };

                channel.BasicConsume(queue: QUEUENAME, autoAck: true, consumer: consumer);
            }
        }
    }
}