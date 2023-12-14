using TechLanches.Domain.ValueObjects;

namespace TechLanches.UnitTests.Domain
{
    [Trait("Domain", "Pedido")]
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

        [Fact(DisplayName = "Criar item do pedido com sucesso")]
        public void CriarItemPedido_DeveRetornarSucesso()
        {
            //Arrange    
            var produtoId = 1;
            var quantidade = 1;
            var precoProduto = 11;

            //Act 
            var itemPedido = new ItemPedido(produtoId, quantidade, precoProduto);

            //Assert
            Assert.NotNull(itemPedido);
            Assert.Equal(produtoId, itemPedido.ProdutoId);
            Assert.Equal(quantidade, itemPedido.Quantidade);
            Assert.Equal(precoProduto, itemPedido.PrecoProduto);
            Assert.Equal(quantidade * precoProduto, itemPedido.Valor);
        }

        [Theory(DisplayName = "Criar item do pedido com falha")]
        [InlineData(1, 0, 11)]
        [InlineData(1, 1, 0)]
        public void CriarItemPedido_Invalido_DeveLancarException(int produtoId, int quantidade, decimal precoProduto)
        {
            //Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new ItemPedido(produtoId, quantidade, precoProduto));
        }

        [Fact(DisplayName = "Criar item do pedido com valor valido")]
        public void CalcularValorItemPedido_Valido_DeveCalcularValorCorretamente()
        {
            //Arrange    
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;

            //Act
            var itemPedido = new ItemPedido(produtoId, quantidade, precoProduto);

            //Assert
            Assert.Equal(30, itemPedido.Valor);
        }

        [Fact(DisplayName = "Criar um pedido com sucesso")]
        public void CriarPedido_DeveRetornarSucesso()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 1;
            var precoProduto = 1;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);

            //Assert
            Assert.NotNull(pedido);
            Assert.Equal(clienteId, pedido.ClienteId);
            Assert.Equal(StatusPedido.PedidoCriado, pedido.StatusPedido);
            Assert.True(pedido.ClienteIdentificado);
            Assert.True(pedido.ItensPedido.Any());
        }

        [Fact(DisplayName = "Criar um pedido com falha")]
        public void CriarPedido_Invalido_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var itensPedido = new List<ItemPedido>() { };

            //Act & Assert
            Assert.Throws<DomainException>(() => new Pedido(clienteId, itensPedido));
        }

        [Fact(DisplayName = "Criar um pedido com valor valido")]
        public void CalcularValorPedido_Valido_DeveCalcularValorCorretamente()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);

            //Assert
            Assert.Equal(30, pedido.Valor);
        }

        [Fact(DisplayName = "Trocar o status do pedido com sucesso")]
        public void TrocarStatus_ComStatusValido_DeveRetornarSucesso()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoCancelado);

            //Assert
            Assert.Equal(StatusPedido.PedidoCancelado, pedido.StatusPedido);
        }

        [Fact(DisplayName = "Trocar o status do pedido para preparação com falha")]
        public void TrocarStatus_ParaPreparacao_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoPronto);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao));
        }

        [Fact(DisplayName = "Trocar o status do pedido para pronto com falha")]
        public void TrocarStatus_ParaPronto_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoPronto);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRetirado);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoFinalizado);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao));
        }

        [Fact(DisplayName = "Trocar o status do pedido para retirado com falha")]
        public void TrocarStatus_ParaRetirado_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoPronto);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRetirado);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoFinalizado);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRetirado));
        }

        [Fact(DisplayName = "Trocar o status do pedido para descartado com falha")]
        public void TrocarStatus_ParaDescartado_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoPronto);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRetirado);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoFinalizado);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoDescartado));
        }

        [Fact(DisplayName = "Trocar o status do pedido para cancelado com falha")]
        public void TrocarStatus_ParaCancelado_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoCancelado));
        }

        [Fact(DisplayName = "Trocar o status do pedido para finalizado com falha")]
        public void TrocarStatus_ParaFinalizado_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoFinalizado));
        }

        [Fact(DisplayName = "Trocar o status do pedido para recebido com falha")]
        public void TrocarStatus_ParaRecebido_DeveLancarException()
        {
            //Arrange    
            var clienteId = 1;
            var produtoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, itensPedido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoEmPreparacao);
            pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoPronto);

            //Assert
            Assert.Throws<DomainException>(() => pedido.TrocarStatus(_statusPedidoValidacaoService, StatusPedido.PedidoRecebido));
        }
    }
}
