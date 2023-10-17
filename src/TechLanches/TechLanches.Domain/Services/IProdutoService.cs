using TechLanches.Domain.Aggregates;

namespace TechLanches.Domain.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<List<Produto>> BuscarPorCategoria(int categoriaId);
        Task<Produto> Cadastrar(string nome, string descricao, decimal preco, int categoriaId);
        Task Atualizar(int produtoId, string nome, string descricao, decimal preco, int categoriaId);
        Task Deletar(int produtoId);
    }
}