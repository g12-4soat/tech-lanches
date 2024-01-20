namespace TechLanches.UnitTests.Domain
{
    [Trait("Domain", "ProdutoTest")]
    public class ProdutoTest
    {
        [Fact]
        public void CriarProduto_DeveRetornarSucesso()
        {
            // Arrange
            string nome = "Produto Teste";
            string descricao = "Descrição do produto teste";
            decimal preco = 10.0M;
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
        public void CriarProduto_Invalido_DeveLancarException(string nome, string descricao, decimal preco, int categoriaId)
        {
            // Arrange, Act & Assert
            Assert.Throws<DomainException>(() => new Produto(nome, descricao, preco, categoriaId));
        }

        [Fact]
        public void AtualizarProduto_Valido_DeveRetornarSucesso()
        {
            // Arrange
            string nome = "Produto Teste Atualizado";
            string descricao = "Nova descrição do produto teste";
            decimal preco = 20.0M;
            int categoriaId = 2;

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
        public void AtualizarProduto_Invalido_DeveLancarException(string nome, string descricao, decimal preco, int categoriaId)
        {
            // Arrange, Act, Assert
            Assert.Throws<DomainException>(() => new Produto(nome, descricao, preco, categoriaId));
        }
    }
}
