using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.UnitTests.Domain
{
    [Trait("Domain", "PedidoAggregate")]
    public class PedidoAggregateTest
    {
        [Fact(DisplayName = "Criar item do pedido com sucesso")]
        public void Criar_item_pedido_sucesso() 
        {
            //Arrange    
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 1;
            var precoProduto = 11;

            //Act 
            var itemPedido = new ItemPedido(produtoId, pedidoId, quantidade, precoProduto);

            //Assert
            Assert.NotNull(itemPedido);
        }

        [Fact(DisplayName = "Criar item do pedido com falha")]
        public void Criar_item_pedido_falha() 
        {
            //Arrange    
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 0;
            var precoProduto = 11;

            //Act & Assert
            Assert.Throws<DomainException>(() => new ItemPedido(produtoId, pedidoId, quantidade, precoProduto));
        }

        [Fact(DisplayName = "Criar item do pedido com quantidade inválida")]
        public void Criar_item_pedido_com_quantidade_invalida()
        {
            //Arrange    
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 0;
            var precoProduto = 11;

            //Act
            var exception = Assert.Throws<DomainException>(() => new ItemPedido(produtoId, pedidoId, quantidade, precoProduto));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Quantidade deve ser maior que zero.", exception.Message);
        }

        [Fact(DisplayName = "Criar item do pedido com preço produto inválido")]
        public void Criar_item_pedido_com_preco_produto_invalido()
        {
            //Arrange    
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 1;
            var precoProduto = 0;

            //Act
            var exception = Assert.Throws<DomainException>(() => new ItemPedido(produtoId, pedidoId, quantidade, precoProduto));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("Preço Produto deve ser maior que zero.", exception.Message);
        }

        [Fact(DisplayName = "Criar item do pedido com valor valido")]
        public void Criar_item_pedido_com_valor_valido()
        {
            //Arrange    
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 3;
            var precoProduto = 10;

            //Act
            var itemPedido = new ItemPedido(produtoId, pedidoId, quantidade, precoProduto);

            //Assert
            Assert.Equal(30, itemPedido.Valor);
        }

        [Fact(DisplayName = "Criar um pedido com sucesso")]
        public void Criar_pedido_com_sucesso()
        {
            //Arrange    
            var clienteId = 1;
            var statusPedidoId = 1;
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 1;
            var precoProduto = 1;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, pedidoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, statusPedidoId, itensPedido);

            //Assert
            Assert.NotNull(pedido);
        }

        [Fact(DisplayName = "Criar um pedido com falha")]
        public void Criar_pedido_com_falha()
        {
            //Arrange    
            var clienteId = 1;
            var statusPedidoId = 1;
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 1;
            var precoProduto = 1;
            var itensPedido = new List<ItemPedido>() { };

            //Act & Assert
            Assert.Throws<DomainException>(() => new Pedido(clienteId, statusPedidoId, itensPedido));
        }

        [Fact(DisplayName = "Criar um pedido com valor valido")]
        public void Criar_pedido_com_valor_valido()
        {
            //Arrange    
            var clienteId = 1;
            var statusPedidoId = 1;
            var produtoId = 1;
            var pedidoId = 1;
            var quantidade = 3;
            var precoProduto = 10;
            var itensPedido = new List<ItemPedido>() { new ItemPedido(produtoId, pedidoId, quantidade, precoProduto) };

            //Act
            var pedido = new Pedido(clienteId, statusPedidoId, itensPedido);

            //Assert
            Assert.Equal(30, pedido.Valor);
        }
    }
}
