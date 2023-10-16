using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application
{
    public class ProdutoService : IProdutoService
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task Atualizar(int produtoId, string nome, string descricao, double preco, int categoriaId)
        {
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
            var produto = await _produtoRepository.BuscarPorId(produtoId);// se for nulo, lançaremos exception?
            return produto;
        }

        public async Task<List<Produto>> BuscarTodos()
        {
            return await _produtoRepository.BuscarTodos();
        }

        public async Task<Produto> Cadastrar(string nome, string descricao, double preco, int categoriaId)
        {
            var produto = new Produto(nome, descricao, preco, categoriaId);

            var novoProduto = await _produtoRepository.Cadastrar(produto);
            await _produtoRepository.UnitOfWork.Commit();

            return novoProduto;
        }
        public async Task Deletar(int produtoId)
        {
            var produto = await BuscarPorId(produtoId);

            if (produto is null)
                throw new Exception($"Produto não encontrado para o id: {produtoId}."); // criar exception customizada?

            _produtoRepository.Deletar(produto);
            await _produtoRepository.UnitOfWork.Commit();
        }
    }
}
