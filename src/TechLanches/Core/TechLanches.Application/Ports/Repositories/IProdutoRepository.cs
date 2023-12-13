using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<Produto> BuscarPorNome(string nome);
        Task<List<Produto>> BuscarPorCategoria(CategoriaProduto categoria);
        Task<Produto> Cadastrar(Produto produto);
    }
}
