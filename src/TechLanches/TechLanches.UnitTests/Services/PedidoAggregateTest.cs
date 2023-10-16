using NSubstitute;
using TechLanches.Application;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.UnitTests.Services
{
    [Trait("Services", "PedidoAggregate")]
    public class PedidoAggregateTest
    {
        [Fact(DisplayName = "Buscar todos pedidos com sucesso")]
        public async Task Buscar_todos_pedidos_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            pedidoRepository.BuscarTodosPedidos().Returns(new List<Pedido> 
            { 
                new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1, 1) })
            });
            var pedidoService = new PedidoService(pedidoRepository);

            //Act 
            var pedidos = await pedidoService.BuscarTodosPedidos();

            //Assert
            await pedidoRepository.Received().BuscarTodosPedidos();
            Assert.NotNull(pedidos);
            Assert.True(pedidos.Any());
        }

        [Fact(DisplayName = "Buscar pedido por id com sucesso")]
        public async Task Buscar_pedido_por_id_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            pedidoRepository.BuscarPedidoPorId(1).Returns(new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1, 1) }));
            var pedidoService = new PedidoService(pedidoRepository);

            //Act 
            var pedido = await pedidoService.BuscarPedidoPorId(1);

            //Assert
            await pedidoRepository.Received().BuscarPedidoPorId(1);
            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.Valor);
        }

        [Fact(DisplayName = "Buscar pedidos por status com sucesso")]
        public async Task Buscar_pedidos_por_status_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            pedidoRepository.BuscarPedidosPorStatus(StatusPedido.PedidoEmPreparacao).Returns(new List<Pedido>
            {
                new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1, 1) })
            });
            var pedidoService = new PedidoService(pedidoRepository);

            //Act 
            var pedidos = await pedidoService.BuscarPedidosPorStatus(StatusPedido.PedidoEmPreparacao);

            //Assert
            await pedidoRepository.Received().BuscarPedidosPorStatus(StatusPedido.PedidoEmPreparacao);
            Assert.NotNull(pedidos);
            Assert.True(pedidos.Any());
        }

        [Fact(DisplayName = "Deve cadastrar pedido com sucesso")]
        public async Task Cadastra_pedido_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pedidoService = new PedidoService(pedidoRepository);

            //Act 
            //var pedido = await pedidoService.Cadastrar(); Implementar

            //Assert
            //Assert.NotNull(pedido);
        }
    }
}
