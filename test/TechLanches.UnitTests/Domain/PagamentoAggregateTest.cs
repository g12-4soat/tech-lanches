using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.UnitTests.Domain
{
    [Trait("Domain", "PagamentoAggregate")]
    public class PagamentoAggregateTest
    {
        [Fact(DisplayName = "Criar pagamento com sucesso")]
        public void Criar_pagamento_sucesso()
        {
            //Arrange    
            var pedidoId = 1;
            var valor = 100;

            //Act 
            var pagamento = new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago);

            //Assert
            Assert.NotNull(pagamento);
        }

        [Fact(DisplayName = "Criar pagamento com falha")]
        public void Criar_pagamento_falha()
        {
            //Arrange    
            var pedidoId = 0;
            var valor = 100;

            //Act & Assert
            Assert.Throws<DomainException>(() => new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago));
        }

        [Fact(DisplayName = "Criar pagamento com valor inválido")]
        public void Criar_pagamento_com_valor_invalido()
        {
            //Arrange    
            var pedidoId = 1;
            var valor = 0;

            //Act
            var exception = Assert.Throws<DomainException>(() => new Pagamento(pedidoId, valor, FormaPagamento.QrCodeMercadoPago));

            //Assert
            Assert.NotNull(exception);
            Assert.Equal("O valor deve ser maior que zero.", exception.Message);
        }
    }
}
