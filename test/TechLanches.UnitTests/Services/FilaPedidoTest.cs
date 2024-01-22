using NSubstitute;
using TechLanches.Application.Controllers;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Presenters;


namespace TechLanches.UnitTests.Services
{
    [Trait("Services", "FilaPedido")]
    public class FilaPedidoTest
    {
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;

        public FilaPedidoTest()
        {
            var validacoes = new List<IStatusPedidoValidacao>
            {
                new StatusPedidoCriadoValidacao(),
                new StatusPedidoCanceladoValidacao(),
                new StatusPedidoEmPreparacaoValidacao(),
                new StatusPedidoDescartadoValidacao(),
                new StatusPedidoFinalizadoValidacao(),
                new StatusPedidoProntoValidacao(),
                new StatusPedidoRecebidoValidacao(),
                new StatusPedidoRetiradoValidacao()
            };

            _statusPedidoValidacaoService = new StatusPedidoValidacaoService(validacoes);
        }

        [Fact]
        public async Task RetornarPrimeiroPedidoDaFila_ComPedidosNaFila_DeveRetornarPedido()
        {
            // Arrange
            var mockFilaPedidoRepository = Substitute.For<IFilaPedidoRepository>();
            var mockPedidoRepository = Substitute.For<IPedidoRepository>();

            mockFilaPedidoRepository.RetornarFilaPedidos().Returns(Task.FromResult(new List<FilaPedido>
            {
                new FilaPedido { PedidoId = 1 },
                new FilaPedido { PedidoId = 2 }
            }));

            mockPedidoRepository.BuscarPorId(1).Returns(Task.FromResult(new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) })));

            var filaPedidoController = new FilaPedidoController(new PedidoGateway(mockPedidoRepository), new PedidoPresenter(), new FilaPedidoGateway(mockFilaPedidoRepository), _statusPedidoValidacaoService);

            // Act
            var pedido = await filaPedidoController.RetornarPrimeiroPedidoDaFila();

            // Assert
            Assert.NotNull(pedido);
        }

        [Fact]
        public async Task RetornarPrimeiroPedidoDaFila_ComFilaVazia_DeveRetornarNull()
        {
            // Arrange
            var mockFilaPedidoRepository = Substitute.For<IFilaPedidoRepository>();
            var mockPedidoRepository = Substitute.For<IPedidoRepository>();

            mockFilaPedidoRepository.RetornarFilaPedidos().Returns(Task.FromResult(new List<FilaPedido>()));
            var filaPedidoController = new FilaPedidoController(new PedidoGateway(mockPedidoRepository), new PedidoPresenter(), new FilaPedidoGateway(mockFilaPedidoRepository), _statusPedidoValidacaoService);

            // Act
            var result = await filaPedidoController.RetornarPrimeiroPedidoDaFila();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TrocarStatus_DeveAtualizarStatusDoPedidoECommit()
        {
            // Arrange
            var mockFilaPedidoRepository = Substitute.For<IFilaPedidoRepository>();
            var mockPedidoRepository = Substitute.For<IPedidoRepository>();

            var pedido = new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) });

            var filaPedidoController = new FilaPedidoController(new PedidoGateway(mockPedidoRepository), new PedidoPresenter(), new FilaPedidoGateway(mockFilaPedidoRepository), _statusPedidoValidacaoService);

            // Act
            await filaPedidoController.TrocarStatus(pedido, StatusPedido.PedidoRecebido);

            // Assert
            Assert.Equal(StatusPedido.PedidoRecebido, pedido.StatusPedido);
            await mockPedidoRepository.Received(1).UnitOfWork.CommitAsync();
        }
    }
}
