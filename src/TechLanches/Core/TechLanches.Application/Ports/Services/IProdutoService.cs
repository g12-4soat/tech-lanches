using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<List<Produto>> BuscarPorCategoria(int categoriaId);
        Task<Produto> Cadastrar(string nome, string descricao, decimal preco, int categoriaId);
        Task Atualizar(int produtoId, string nome, string descricao, decimal preco, int categoriaId);
        Task Deletar(Produto produto);
    }
}