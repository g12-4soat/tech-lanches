using NSubstitute;

namespace TechLanches.UnitTests.Services
{
    [Trait("Services", "Produto")]
    public class ProdutoTest
    {
        [Fact(DisplayName = "Deve cadastrar produto com sucesso")]
        public async Task CadastrarProduto_DeveRetornarSucesso()
        {
            // Arrange
            string nome = "Produto Teste";
            string descricao = "Descrição do produto teste";
            decimal preco = 10.0M;
            int categoriaId = 1;

            var produto = new Produto(nome, descricao, preco, categoriaId);
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.Cadastrar(produto).Returns(new Produto(nome, descricao, preco, categoriaId));

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var novoProduto = await produtoService.Cadastrar(nome, descricao, preco, categoriaId);

            // Assert
            Assert.NotNull(novoProduto);
            Assert.Equal(nome, novoProduto.Nome);
            Assert.Equal(descricao, novoProduto.Descricao);
            Assert.Equal(preco, novoProduto.Preco);
            Assert.Equal(categoriaId, novoProduto.Categoria.Id);
        }

        [Fact(DisplayName = "Deve atualizar produto com sucesso")]
        public async Task AtualizarProduto_DeveRetornarSucesso()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            await produtoService.Atualizar(1, "Novo Nome", "Nova Descrição", 20, 2);

            // Assert
            produtoRepository.Received(1).Atualizar(Arg.Any<Produto>());
            await unitOfWork.Received(1).Commit();
        }

        [Fact(DisplayName = "Deve buscar produtos por categoria com sucesso")]
        public async Task BuscarPorCategoria_DeveRetornarProdutosComCategoriaSolicitada()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorCategoria(new CategoriaProduto(1, "teste")).Returns(new List<Produto> { new ("Nome", "Descrição do produto", 20.0m, 2) });

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var listaDeProdutos = await produtoService.BuscarPorCategoria(1);

            // Assert
            await produtoRepository.Received(1).BuscarPorCategoria(Arg.Any<CategoriaProduto>());
            Assert.NotNull(listaDeProdutos);
        }

        [Fact(DisplayName = "Deve buscar produto por id com sucesso")]
        public async Task BuscarPorId_DeveRetornarProdutoSolicitado()
        {
            // Arrange
            var produto = new Produto("Nome", "Descrição do produto", 20, 2);
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorId(1).Returns(produto);

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var produtoAtualizado = await produtoService.BuscarPorId(1);

            // Assert
            await produtoRepository.Received(1).BuscarPorId(1);
            Assert.NotNull(produtoAtualizado);
            Assert.IsType<Produto>(produtoAtualizado);
            Assert.Equal(produto.Nome, produtoAtualizado.Nome);
        }

        [Fact(DisplayName = "Deve buscar todos produtos com sucesso")]
        public async Task BuscarTodos_DeveRetornarTodosProdutos()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarTodos().Returns(new List<Produto> { new ("Nome", "Descrição do produto", 20, 2) });

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var listaDeProdutos = await produtoService.BuscarTodos();

            // Assert
            await produtoRepository.Received(1).BuscarTodos();
            Assert.NotNull(listaDeProdutos);
            Assert.IsType<List<Produto>>(listaDeProdutos);
        }

        [Fact(DisplayName = "Deve deletar o produto com sucesso")]
        public async Task Deletar_ProdutoEncontrado_DeveDeletarProdutoComSucesso()
        {
            // Arrange
            var produto = new Produto("Nome", "Descrição do produto", 20, 2);
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorId(1).Returns(produto);

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            await produtoService.Deletar(produto);

            // Assert
            await unitOfWork.Received(1).Commit();
            Assert.NotNull(produto);
            Assert.True(produto.Deletado);
        }
    }
}
