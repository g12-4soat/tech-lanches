using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Gateways
{
    public class ProdutoGateway : IProdutoGateway
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutoGateway(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public Task<List<Produto>> BuscarPorCategoria(CategoriaProduto categoria)
        {
            return _produtoRepository.BuscarPorCategoria(categoria);
        }

        public Task<Produto> BuscarPorId(int produtoId)
        {
            return _produtoRepository.BuscarPorId(produtoId);
        }

        public Task<Produto> BuscarPorNome(string nome)
        {
            return _produtoRepository.BuscarPorNome(nome);
        }

        public Task<List<Produto>> BuscarTodos()
        {
            return _produtoRepository.BuscarTodos();
        }

        public Task<Produto> Cadastrar(Produto produto)
        {
            return _produtoRepository.Cadastrar(produto);
        }

        public async Task CommitAsync()
        {
            await _produtoRepository.UnitOfWork.CommitAsync();
        }
    }
}
