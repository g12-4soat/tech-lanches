using TechLanches.Domain.Aggregates;

namespace TechLanches.Domain.Services
{
    public interface IProdutoService
    {
        Task<Produto> Cadastrar(Produto produto);
        Task<Produto> Atualizar(int produtoId);
        Task Deletar(int produtoId);
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<List<Produto>> BuscarPorCategoria(int categoriaId);
    }
}