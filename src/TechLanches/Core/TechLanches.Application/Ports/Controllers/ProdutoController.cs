using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.UseCases.Produtos;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class ProdutoController : IProdutoController
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Atualizar(int produtoId, string nome, string descricao, decimal preco, int categoriaId)
        {
            await ProdutoUseCases.Atualizar(produtoId, nome, descricao, preco, categoriaId, _produtoRepository);

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
            var novoProduto = await ProdutoUseCases.Cadastrar(nome, descricao, preco, categoriaId, _produtoRepository);

            await _produtoRepository.UnitOfWork.Commit();

            return novoProduto;
        }

        public async Task Deletar(Produto produto)
        {
            produto.DeletarProduto();

            await _produtoRepository.UnitOfWork.Commit();
        }
    }
}
