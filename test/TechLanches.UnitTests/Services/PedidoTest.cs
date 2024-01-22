using NSubstitute;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Controllers;
using TechLanches.Application.Gateways;
using TechLanches.Application.Presenters;
using TechLanches.UnitTests.Fixtures;

namespace TechLanches.UnitTests.Services
{
    [Trait("Services", "Pedido")]
    public class PedidoTest : IClassFixture<PedidoFixture>
    {
        private readonly PedidoFixture _pedidoFixture;

        public PedidoTest(PedidoFixture pedidoFixture)
        {
            _pedidoFixture = pedidoFixture;
        }

        [Fact(DisplayName = "Buscar todos pedidos com sucesso")]
        public async Task BuscarTodos_DeveRetornarTodosPedidos()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();

            pedidoRepository.BuscarTodos().Returns(_pedidoFixture.GerarPedidosValidos());
            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            //Act 
            var pedidos = await pedidoController.BuscarTodos();

            //Assert
            await pedidoRepository.Received().BuscarTodos();
            Assert.NotNull(pedidos);
            Assert.True(pedidos.Any());
        }

        [Fact(DisplayName = "Buscar pedido por id com sucesso")]
        public async Task BuscarPorId_DeveRetornarPedidoSolicitado()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();

            pedidoRepository.BuscarPorId(1).Returns(_pedidoFixture.GerarPedidoValido());
            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            //Act 
            var pedido = await pedidoController.BuscarPorId(1);

            //Assert
            await pedidoRepository.Received().BuscarPorId(1);
            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.Valor);
        }

        [Fact(DisplayName = "Buscar pedidos por status com sucesso")]
        public async Task BuscarPorStatus_DeveRetornarPedidoComStatusSolicitado()
        {
            //Arrange    
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();

            pedidoRepository.BuscarPorStatus(StatusPedido.PedidoEmPreparacao).Returns(_pedidoFixture.GerarPedidosValidos());
            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            //Act 
            var pedidos = await pedidoController.BuscarPorStatus(StatusPedido.PedidoEmPreparacao);

            //Assert
            await pedidoRepository.Received().BuscarPorStatus(StatusPedido.PedidoEmPreparacao);
            Assert.NotNull(pedidos);
            Assert.True(pedidos.Any());
        }

        [Fact(DisplayName = "Deve trocar status com sucesso")]
        public async Task TrocarStatus_ComStatusValido_DeveRetornarSucesso()
        {
            //Arrange    
            const int PEDIDO_ID = 1;
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();
            var pedidoEditar = _pedidoFixture.GerarPedidoSemClienteValido();

            pedidoRepository.BuscarPorId(PEDIDO_ID).Returns(pedidoEditar);
            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            //Act 
            var pedido = await pedidoController.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoRecebido);

            //Assert
            await pedidoRepository.Received().BuscarPorId(PEDIDO_ID);
            Assert.NotNull(pedido);

        }

        [Fact(DisplayName = "Trocar status pedido inexistente com falha")]
        public async Task TrocarStatus_ComPedidoInexistente_DeveLancarException()
        {
            //Arrange    
            const int PEDIDO_ID = 1;
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();

            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            //Act 
            var exception = await Assert.ThrowsAsync<DomainException>(async () => await pedidoController.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoEmPreparacao));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Não foi encontrado nenhum pedido com id informado.", exception.Message);
        }

        [Fact(DisplayName = "Trocar status pedido com falha")]
        public async Task TrocarStatus_ComStatusInvalido_DeveLancarException()
        {
            //Arrange    
            const int PEDIDO_ID = 1;
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();
            var pedidoEditar = _pedidoFixture.GerarPedidoSemClienteValido();

            pedidoRepository.BuscarPorId(PEDIDO_ID).Returns(pedidoEditar);
            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            //Act 
            var exception = await Assert.ThrowsAsync<DomainException>(async () => await pedidoController.TrocarStatus(PEDIDO_ID, StatusPedido.PedidoFinalizado));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("O status selecionado não é válido", exception.Message);
        }

        [Fact(DisplayName = "Deve cadastrar pedido com sucesso")]
        public async Task CadastrarPedido_DeveRetornarSucesso()
        {
            //Arrange
            const string CPF = "046.047.173-20";
            var itensPedidos = _pedidoFixture.GerarItensPedidoValidos();
            var pedidoReturn = new Pedido(1, itensPedidos);
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var clienteService = Substitute.For<IClienteService>();
            var rabbitMqService = Substitute.For<IRabbitMqService>();
            clienteService.BuscarPorCpf(CPF).Returns(new Cliente("Joao", "joao@gmail.com", CPF));
            var pedidoController = new PedidoController(new PedidoGateway(pedidoRepository), new PedidoPresenter(), clienteService, _pedidoFixture.StatusPedidoValidacaoService, rabbitMqService);

            pedidoRepository.Cadastrar(pedidoReturn).Returns(pedidoReturn);

            //Act 
            var pedido = await pedidoController.Cadastrar(CPF, itensPedidos);

            //Assert
            await pedidoRepository.Received().Cadastrar(pedidoReturn);
            Assert.NotNull(pedido);
            Assert.Equal(1, pedido.ClienteId);
        }
    }
}
