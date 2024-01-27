using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Gateways.Interfaces
{
    public interface IProdutoGateway : IRepositoryGateway
    {
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<Produto> BuscarPorNome(string nome);
        Task<List<Produto>> BuscarPorCategoria(CategoriaProduto categoria);
        Task<Produto> Cadastrar(Produto produto);
    }
}
