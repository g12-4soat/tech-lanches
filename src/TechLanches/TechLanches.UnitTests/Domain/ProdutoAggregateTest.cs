using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.UnitTests.Domain
{
    [Trait("Domain", "ProdutoAggregate")]
    public class ProdutoAggregateTest
    {
        public ProdutoAggregateTest()
        { }

        [Fact(DisplayName = "Criar produto com sucesso")]
        public void Criar_produto_sucesso()
        {
            //Arrange    
            var nome = "X-Tudo";
            var descricao = "Lanche completo com molho especial";
            var preco = 20;
            var categoriaId = 1;
            

            //Act 
            var produto = new Produto(nome, descricao, preco, categoriaId);

            //Assert
            Assert.NotNull(produto);
        }

        [Fact(DisplayName = "Criar produto com falha")]
        public void Criar_produto_falha()
        {
            //Arrange    
            var nome = "";
            var descricao = "Lanche completo com molho especial";
            var preco = 20;
            var categoriaId = 1;


            //Act - Assert
            Assert.Throws<DomainException>(() => new Produto(nome, descricao, preco, categoriaId));
        }
    }
}
