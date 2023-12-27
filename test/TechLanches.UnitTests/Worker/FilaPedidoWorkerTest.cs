using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using TechLanches.Adapter.FilaPedidos;
using TechLanches.Adapter.FilaPedidos.Options;

namespace TechLanches.UnitTests.Worker
{
    [Trait("Worker", "FilaPedidoWorker")]
    public class FilaPedidoWorkerTest
    {
        [Fact]
        public async Task ExecuteAsync_ComProximoPedido_DeveProcessarPedido()
        {
            // Arrange
            var mockFilaPedidoService = Substitute.For<IFilaPedidoService>();
            var mockPedidoService = Substitute.For<IPedidoService>();
            mockFilaPedidoService.RetornarPrimeiroPedidoDaFila().Returns(new Pedido(1, new List<ItemPedido> { new (1, 1, 1) }));
            var mockLogger = Substitute.For<ILogger<FilaPedidosHostedService>>();

            var mockOptions = Substitute.For<IOptions<WorkerOptions>>();
            var workerOptions = new WorkerOptions
            {
                DelayPreparacaoPedidoEmSegundos = 1,
                DelayVerificacaoFilaEmSegundos = 5
            };

            mockOptions.Value.Returns(workerOptions);

            var workerService = new FilaPedidosHostedService(mockLogger, mockFilaPedidoService, mockOptions, mockPedidoService);

            // Act
            await workerService.StartAsync(CancellationToken.None);
            await Task.Delay(10000);

            // Assert
            await mockFilaPedidoService.Received(1).RetornarPrimeiroPedidoDaFila();
            await mockFilaPedidoService.Received(1).TrocarStatus(Arg.Any<Pedido>(), StatusPedido.PedidoEmPreparacao);
            await mockFilaPedidoService.Received(1).TrocarStatus(Arg.Any<Pedido>(), StatusPedido.PedidoPronto);
        }

        [Fact]
        public async Task ExecuteAsync_SemProximoPedido_NaoDeveProcessar()
        {
            // Arrange
            var mockFilaPedidoService = Substitute.For<IFilaPedidoService>();
            mockFilaPedidoService.RetornarPrimeiroPedidoDaFila().ReturnsNull();
            var mockPedidoService = Substitute.For<IPedidoService>();

            var mockLogger = Substitute.For<ILogger<FilaPedidosHostedService>>();

            var mockOptions = Substitute.For<IOptions<WorkerOptions>>();
            var workerOptions = new WorkerOptions
            {
                DelayPreparacaoPedidoEmSegundos = 1,
                DelayVerificacaoFilaEmSegundos = 1
            };

            mockOptions.Value.Returns(workerOptions);

            var workerService = new FilaPedidosHostedService(mockLogger, mockFilaPedidoService, mockOptions, mockPedidoService);

            // Act
            await workerService.StartAsync(CancellationToken.None);
            await Task.Delay(1500);

            // Assert
            await mockFilaPedidoService.Received(1).RetornarPrimeiroPedidoDaFila();
            await mockFilaPedidoService.DidNotReceive().TrocarStatus(Arg.Any<Pedido>(), Arg.Any<StatusPedido>());
        }

        [Fact]
        public async Task ExecuteAsync_ComExcecao_NoCatch_DeveLogarErro()
        {
            // Arrange
            var mockFilaPedidoService = Substitute.For<IFilaPedidoService>();
            var mockPedidoService = Substitute.For<IPedidoService>();
            mockFilaPedidoService.RetornarPrimeiroPedidoDaFila().Throws(x => throw new Exception("Simulando erro"));

            var mockLogger = Substitute.For<ILogger<FilaPedidosHostedService>>();
            var mockOptions = Substitute.For<IOptions<WorkerOptions>>();
            var workerOptions = new WorkerOptions
            {
                DelayPreparacaoPedidoEmSegundos = 1,
                DelayVerificacaoFilaEmSegundos = 1
            };

            mockOptions.Value.Returns(workerOptions);
            var workerService = new FilaPedidosHostedService(mockLogger, mockFilaPedidoService, mockOptions, mockPedidoService);

            // Act
            await workerService.StartAsync(CancellationToken.None);
            await Task.Delay(1500);

            // Assert
            await mockFilaPedidoService.Received(1).RetornarPrimeiroPedidoDaFila();
            await mockFilaPedidoService.DidNotReceive().TrocarStatus(Arg.Any<Pedido>(), Arg.Any<StatusPedido>());
            mockLogger.Received(1)
                .Log(LogLevel.Error, 
                     Arg.Any<EventId>(), 
                     Arg.Any<object>(), 
                     Arg.Any<Exception>(),
                     Arg.Any<Func<object, 
                     Exception?, 
                     string>>());
        }
    }
}
