using NSubstitute;
using TechLanches.Application;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
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
            var pagamentoService = Substitute.For<IPagamentoService>();

            pedidoRepository.BuscarTodos().Returns(new List<Pedido> 
            { 
                new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) })
            });
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);

            //Act 
            var pedidos = await pedidoService.BuscarTodos();

            //Assert
            await pedidoRepository.Received().BuscarTodos();
            Assert.NotNull(pedidos);
            Assert.True(pedidos.Any());
        }

        [Fact(DisplayName = "Buscar pedido por id com sucesso")]
        public async Task Buscar_pedido_por_id_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();

            pedidoRepository.BuscarPorId(1).Returns(new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) }));
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);

            //Act 
            var pedido = await pedidoService.BuscarPorId(1);

            //Assert
            await pedidoRepository.Received().BuscarPorId(1);
            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.Valor);
        }

        [Fact(DisplayName = "Buscar pedidos por status com sucesso")]
        public async Task Buscar_pedidos_por_status_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();

            pedidoRepository.BuscarPorStatus(StatusPedido.PedidoEmPreparacao).Returns(new List<Pedido>
            {
                new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) })
            });
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);

            //Act 
            var pedidos = await pedidoService.BuscarPorStatus(StatusPedido.PedidoEmPreparacao);

            //Assert
            await pedidoRepository.Received().BuscarPorStatus(StatusPedido.PedidoEmPreparacao);
            Assert.NotNull(pedidos);
            Assert.True(pedidos.Any());
        }

        [Fact(DisplayName = "Deve trocar status com sucesso")]
        public async Task Trocar_status_com_sucesso()
        {
            //Arrange    
            const int PEDIDO_ID = 1;
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();
            var pedidoEditar = new Pedido(null, new List<ItemPedido>() { new ItemPedido(1, 1, 1) });

            pedidoRepository.BuscarPorId(PEDIDO_ID).Returns(pedidoEditar);
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);

            //Act 
            var pedido = await pedidoService.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoEmPreparacao);

            //Assert
            await pedidoRepository.Received().BuscarPorId(PEDIDO_ID);
            Assert.NotNull(pedido);

        }

        [Fact(DisplayName = "Trocar status pedido inexistente com falha")]
        public async Task Trocar_status_pedido_inexistente_com_falha()
        {
            //Arrange    
            const int PEDIDO_ID = 1;
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();

            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);

            //Act 
            var exception = await Assert.ThrowsAsync<DomainException>(async () => await pedidoService.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoEmPreparacao));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Não foi encontrado nenhum pedido com id informado.", exception.Message);
        }

        [Fact(DisplayName = "Trocar status pedido com falha")]
        public async Task Trocar_status_pedido_com_falha()
        {
            //Arrange    
            const int PEDIDO_ID = 1;
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();
            var pedidoEditar = new Pedido(null, new List<ItemPedido>() { new ItemPedido(1, 1, 1) });

            pedidoRepository.BuscarPorId(PEDIDO_ID).Returns(pedidoEditar);
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);

            //Act 
            var exception = await Assert.ThrowsAsync<DomainException>(async () => await pedidoService.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoCancelado));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("O status selecionado não é válido", exception.Message);
        }

        [Fact(DisplayName = "Deve cadastrar pedido com sucesso")]
        public async Task Cadastra_pedido_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();

            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);
            var itensPedidos = new List<ItemPedido>() { new ItemPedido(1, 1, 1) };

            //Act 
            var pedido = await pedidoService.Cadastrar(1, itensPedidos);

            //Assert
            await pedidoRepository.Received().Cadastrar(pedido);
            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.ClienteId);
        }

        [Fact(DisplayName = "Deve atualizar pedido com sucesso")]
        public async Task Atualiza_pedido_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            pedidoRepository.UnitOfWork.Returns(unitOfWork);

            var pedidoService = new PedidoService(pedidoRepository, pagamentoService);
            var itensPedidos = new List<ItemPedido>() { new ItemPedido(1, 1, 1) };

            //Act 
            await pedidoService.Atualizar(1, 1, itensPedidos);

            //Assert
            pedidoRepository.Received(1).Atualizar(Arg.Any<Pedido>());
            await unitOfWork.Received(1).Commit();
        }
    }
}
