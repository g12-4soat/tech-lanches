using NSubstitute;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.Validations;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.UnitTests.Services
{
    [Trait("Services", "PedidoAggregate")]
    public class PedidoAggregateTest
    {
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;

        public PedidoAggregateTest()
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

        [Fact(DisplayName = "Buscar todos pedidos com sucesso")]
        public async Task Buscar_todos_pedidos_com_sucesso()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();
            var clienteService = Substitute.For<IClienteService>();

            pedidoRepository.BuscarTodos().Returns(new List<Pedido>
            {
                new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) })
            });
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

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
            var clienteService = Substitute.For<IClienteService>();

            pedidoRepository.BuscarPorId(1).Returns(new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) }));
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

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
            var clienteService = Substitute.For<IClienteService>();

            pedidoRepository.BuscarPorStatus(StatusPedido.PedidoEmPreparacao).Returns(new List<Pedido>
            {
                new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1) })
            });
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

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
            var clienteService = Substitute.For<IClienteService>();
            var pedidoEditar = new Pedido(null, new List<ItemPedido>() { new ItemPedido(1, 1, 1) });

            pedidoRepository.BuscarPorId(PEDIDO_ID).Returns(pedidoEditar);
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

            //Act 
            var pedido = await pedidoService.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoRecebido);

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
            var clienteService = Substitute.For<IClienteService>();

            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

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
            var clienteService = Substitute.For<IClienteService>();
            var pedidoEditar = new Pedido(null, new List<ItemPedido>() { new ItemPedido(1, 1, 1) });

            pedidoRepository.BuscarPorId(PEDIDO_ID).Returns(pedidoEditar);
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

            //Act 
            var exception = await Assert.ThrowsAsync<DomainException>(async () => await pedidoService.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoFinalizado));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("O status selecionado não é válido", exception.Message);
        }

        [Fact(DisplayName = "Deve cadastrar pedido com sucesso")]
        public async Task Cadastra_pedido_com_sucesso()
        {
            //Arrange
            const string CPF = "046.047.173-20";
            var itensPedidos = new List<ItemPedido>() { new ItemPedido(1, 1, 1) };
            var pedidoReturn = new Pedido(1, itensPedidos);
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();
            var clienteService = Substitute.For<IClienteService>();
            clienteService.BuscarPorCpf(CPF).Returns(new Cliente("Joao", "joao@gmail.com", CPF));
            pagamentoService.RealizarPagamento(Arg.Any<int>(), FormaPagamento.QrCodeMercadoPago, pedidoReturn.Valor).Returns(true);
            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);

            pedidoRepository.Cadastrar(pedidoReturn).Returns(pedidoReturn);
            //Act 
            var pedido = await pedidoService.Cadastrar(CPF, itensPedidos);

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
            var clienteService = Substitute.For<IClienteService>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            pedidoRepository.UnitOfWork.Returns(unitOfWork);

            var pedidoService = new PedidoService(pedidoRepository, pagamentoService, clienteService, _statusPedidoValidacaoService);
            var itensPedidos = new List<ItemPedido>() { new ItemPedido(1, 1, 1) };

            //Act 
            await pedidoService.Atualizar(1, 1, itensPedidos);

            //Assert
            pedidoRepository.Received(1).Atualizar(Arg.Any<Pedido>());
            await unitOfWork.Received(1).Commit();
        }
    }
}
