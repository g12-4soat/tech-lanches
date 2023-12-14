using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Atualizar(int produtoId, string nome, string descricao, decimal preco, int categoriaId)
        {
            var produtoExistente = await _produtoRepository.BuscarPorNome(nome);

            if (produtoExistente is not null)
                throw new DomainException($"Produto já cadastrado para o nome: {nome}");

            var produto = new Produto(produtoId, nome, descricao, preco, categoriaId);
            _produtoRepository.Atualizar(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }

        public async Task<List<Produto>> BuscarPorCategoria(int categoriaId)
        {
            var categoriaProduto = CategoriaProduto.From(categoriaId);
            return await _produtoRepository.BuscarPorCategoria(categoriaProduto);
        }

        public async Task<Produto> BuscarPorId(int produtoId)
        {
            return await _produtoRepository.BuscarPorId(produtoId);
        }

        public async Task<List<Produto>> BuscarTodos()
        {
            return await _produtoRepository.BuscarTodos();
        }

        public async Task<Produto> Cadastrar(string nome, string descricao, decimal preco, int categoriaId)
        {
            var produtoExistente = await _produtoRepository.BuscarPorNome(nome);

            if (produtoExistente is not null)
                throw new DomainException($"Produto já cadastrado para o nome: {nome}");

            var produto = new Produto(nome, descricao, preco, categoriaId);

            var novoProduto = await _produtoRepository.Cadastrar(produto);
            await _produtoRepository.UnitOfWork.Commit();

            return novoProduto;
        }

        public async Task Deletar(Produto produto)
        {
            produto.ProdutoDeletado();
            await _produtoRepository.UnitOfWork.Commit();
        }
    }
}
