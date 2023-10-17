﻿using NSubstitute;
using TechLanches.Application;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.UnitTests.Services
{
    public class ProdutoAggregateTest
    {
        [Fact]
        public async Task Cadastrar_Com_Sucesso()
        {
            string nome = "Nome";
            string descricao = "Descrição";
            double preco = 10.0;
            int categoriaId = 1;

            var produto = new Produto(nome, descricao, preco, categoriaId);
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.Cadastrar(produto).Returns(new Produto(nome, descricao,preco, categoriaId));

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

        [Fact]
        public async Task Atualizar_Com_Sucesso()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            await produtoService.Atualizar(1, "Novo Nome", "Nova Descrição", 20.0, 2);

            // Assert
            produtoRepository.Received(1).Atualizar(Arg.Any<Produto>());
            await unitOfWork.Received(1).Commit();
        }
        [Fact]
        public async Task Buscar_Por_Categoria_Com_Sucesso()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorCategoria(new CategoriaProduto(1,"teste")).Returns(new List<Produto> { new Produto("Nome", "Descrição", 20.0, 2) });

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var listaDeProdutos = await produtoService.BuscarPorCategoria(1);

            // Assert
            await produtoRepository.Received(1).BuscarPorCategoria(Arg.Any<CategoriaProduto>());
            Assert.NotNull(listaDeProdutos);
        }

        [Fact]
        public async Task Busca_Por_Id_Com_Sucesso()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorId(1).Returns( new Produto("Nome", "Descrição", 20.0, 2));

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var produto = await produtoService.BuscarPorId(1);

            // Assert
            await produtoRepository.Received(1).BuscarPorId(1);
            Assert.NotNull(produto);
            Assert.IsType<Produto>(produto);
            Assert.Equal("Nome", produto.Nome);
        }
        [Fact]
        public async Task BuscarTodos_Com_Sucesso()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarTodos().Returns(new List<Produto> { new Produto("Nome", "Descrição", 20.0, 2) });


            var produtoService = new ProdutoService(produtoRepository);

            // Act
            var listaDeProdutos = await produtoService.BuscarTodos();

            // Assert
            await produtoRepository.Received(1).BuscarTodos();
            Assert.NotNull(listaDeProdutos);
            Assert.IsType<List<Produto>>(listaDeProdutos);
        }
        [Fact]
        public async Task Deletar_ProdutoEncontrado_Com_Sucesso()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorId(1).Returns(new Produto("Nome", "Descrição", 20.0, 2)); // Produto encontrado

            var produtoService = new ProdutoService(produtoRepository);

            // Act
            await produtoService.Deletar(1);

            // Assert
            produtoRepository.Received(1).Deletar(Arg.Any<Produto>());
            await unitOfWork.Received(1).Commit();
        }

        [Fact]
        public async Task Deletar_ProdutoNaoEncontrado_DeveLancarException()
        {
            // Arrange
            var produtoRepository = Substitute.For<IProdutoRepository>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            produtoRepository.UnitOfWork.Returns(unitOfWork);
            produtoRepository.BuscarPorId(1).Returns(Task.FromResult<Produto>(null)); // Produto não encontrado

            var produtoService = new ProdutoService(produtoRepository);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => produtoService.Deletar(1));
        }
    }
}