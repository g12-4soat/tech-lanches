using NSubstitute;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.UnitTests.Services
{
    public class FilaPedidoAggregateTest
    {
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

            var filaPedidoService = new FilaPedidoService(mockPedidoRepository, mockFilaPedidoRepository);

            // Act
            var pedido = await filaPedidoService.RetornarPrimeiroPedidoDaFila();

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

            var filaPedidoService = new FilaPedidoService(mockPedidoRepository, mockFilaPedidoRepository);

            // Act
            var result = await filaPedidoService.RetornarPrimeiroPedidoDaFila();

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

            var filaPedidoService = new FilaPedidoService(mockPedidoRepository, mockFilaPedidoRepository);

            // Act
            await filaPedidoService.TrocarStatus(pedido, StatusPedido.PedidoEmPreparacao);

            // Assert
            Assert.Equal(StatusPedido.PedidoEmPreparacao, pedido.StatusPedido);
            await mockPedidoRepository.Received(1).UnitOfWork.Commit();
        }
    }
}
