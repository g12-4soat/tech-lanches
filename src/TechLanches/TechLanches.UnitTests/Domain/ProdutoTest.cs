using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.UnitTests.Domain
{
    public class ProdutoTest
    {
        [Fact]
        public void CriarProduto_ComValoresValidos_DevePassar()
        {
            // Arrange
            string nome = "Produto Teste";
            string descricao = "Descrição do produto teste";
            double preco = 10.0;
            int categoriaId = 1;

            // Act
            var produto = new Produto(nome, descricao, preco, categoriaId);

            // Assert
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(descricao, produto.Descricao);
            Assert.Equal(preco, produto.Preco);
            Assert.Equal(categoriaId, produto.Categoria.Id);
        }

        [Theory]
        [InlineData("Produto Teste", "Descrição", -1.0, 1)]
        [InlineData("Produto Teste", "Descrição", 10.0, 0)]
        public void CriarProduto_ComValoresInvalidos_DeveLancarException(string nome, string descricao, double preco, int categoriaId)
        {
            // Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new Produto(nome, descricao, preco, categoriaId));
        }

        [Fact]
        public void AtualizarProduto_ComValoresValidos_DevePassar()
        {
            // Arrange
            int produtoId = 1;
            string nome = "Produto Teste Atualizado";
            string descricao = "Nova descrição do produto teste";
            double preco = 20.0;
            int categoriaId = 2;

            // Act
            var produto = new Produto(produtoId, nome, descricao, preco, categoriaId);

            // Assert
            Assert.Equal(nome, produto.Nome);
            Assert.Equal(descricao, produto.Descricao);
            Assert.Equal(preco, produto.Preco);
            Assert.Equal(categoriaId, produto.Categoria.Id);
        }

        [Theory]
        [InlineData("Produto Teste", "Descrição", -1.0, 1)]
        [InlineData("Produto Teste", "Descrição", 10.0, 0)]
        public void AtualizarProduto_ComValoresInvalidos_DeveLancarException(string nome, string descricao, double preco, int categoriaId)
        {
            // Arrange
            int produtoId = 1;

            // Arrange, Assert
            Assert.Throws<DomainException>(() => new Produto(produtoId, nome, descricao, preco, categoriaId));
        }
    }
}
