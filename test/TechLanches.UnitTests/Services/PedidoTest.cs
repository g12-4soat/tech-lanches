using NSubstitute;

namespace TechLanches.UnitTests.Services
{
    [Trait("Services", "Pedido")]
    public class PedidoTest
    {
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;

        public PedidoTest()
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
        public async Task BuscarTodos_DeveRetornarTodosPedidos()
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
        public async Task BuscarPorId_DeveRetornarPedidoSolicitado()
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
        public async Task BuscarPorStatus_DeveRetornarPedidoComStatusSolicitado()
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
        public async Task TrocarStatus_ComStatusValido_DeveRetornarSucesso()
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
        public async Task TrocarStatus_ComPedidoInexistente_DeveLancarException()
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
        public async Task TrocarStatus_ComStatusInvalido_DeveLancarException()
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
        public async Task CadastrarPedido_DeveRetornarSucesso()
        {
            //Arrange
            const string CPF = "046.047.173-20";
            var itensPedidos = new List<ItemPedido>() { new ItemPedido(1, 1, 1) };
            var pedidoReturn = new Pedido(1, itensPedidos);
            var pedidoRepository = Substitute.For<IPedidoRepository>();
            var pagamentoService = Substitute.For<IPagamentoService>();
            var clienteService = Substitute.For<IClienteService>();
            clienteService.BuscarPorCpf(CPF).Returns(new Cliente("Joao", "joao@gmail.com", CPF));
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
        public async Task AtualizarPedido_DeveRetornarSucesso()
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
