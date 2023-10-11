using TechLanches.Domain.Aggregates;

namespace TechLanches.Domain.Repositories
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<List<Produto>> BuscarPorCategoria(int categoriaId);
        Task<Produto> Cadastrar(Produto produto);
        void Atualizar(Produto produto);
        void Deletar(int produtoId);

    }
}
