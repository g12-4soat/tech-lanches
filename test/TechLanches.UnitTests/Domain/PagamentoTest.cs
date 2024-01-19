using TechLanches.Domain.Aggregates;

namespace TechLanches.UnitTests.Domain
{
    [Trait("Domain", "Pagamento")]
    public class PagamentoTest
    {
        [Fact(DisplayName = "Criar pagamento com sucesso")]
        public void CriarPagamento_DeveRetornarSucesso()
        {
            //Arrange    
            var pedidoId = 1;
            var valor = 100;

            //Act 
            var pagamento = new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago);

            //Assert
            Assert.NotNull(pagamento);
            Assert.Equal(pedidoId, pagamento.PedidoId);
            Assert.Equal(valor, pagamento.Valor);
            Assert.Equal(FormaPagamento.QrCodeMercadoPago, pagamento.FormaPagamento);
            Assert.Equal(StatusPagamento.Aguardando, pagamento.StatusPagamento);
        }

        [Theory(DisplayName = "Criar pagamento com falha")]
        [InlineData(0, 100)]
        [InlineData(1, 0)]
        public void CriarPagamento_Invalido_DeveLancarException(int pedidoId, decimal valor)
        {
            //Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago));
        }

        [Fact(DisplayName = "Reprovar pagamento com status aprovado com falha")]
        public void Reprovar_PagamentoAprovado_DeveLancarException()
        {
            //Arrange    
            var pedidoId = 1;
            var valor = 100;
            var pagamentoId = 1;
            var pagamento = new Pagamento(pagamentoId, pedidoId, valor, StatusPagamento.Aprovado);

            //Act, Assert
            Assert.Throws<DomainException>(() => pagamento.Reprovar());
        }

        [Fact(DisplayName = "Aprovar pagamento com sucesso")]
        public void Reprovar_PagamentoAguardando_DeveReprovarComSucesso()
        {
            //Arrange    
            var pedidoId = 1;
            var valor = 100;
            var pagamento = new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago);

            //Act
            pagamento.Reprovar();

            //Act, Assert
            Assert.Equal(StatusPagamento.Reprovado, pagamento.StatusPagamento);
        }

        [Fact(DisplayName = "Aprovar pagamento com sucesso")]
        public void Aprovar_PagamentoAguardando_DeveAprovarComSucesso()
        {
            //Arrange    
            var pedidoId = 1;
            var valor = 100;
            var pagamento = new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago);

            //Act
            pagamento.Aprovar();

            //Act, Assert
            Assert.Equal(StatusPagamento.Aprovado, pagamento.StatusPagamento);
        }
    }
}
