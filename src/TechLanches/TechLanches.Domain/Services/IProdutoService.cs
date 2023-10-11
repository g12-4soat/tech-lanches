using TechLanches.Domain.Aggregates;

namespace TechLanches.Domain.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> BuscarTodos();
        Task<Produto> BuscarPorId(int produtoId);
        Task<List<Produto>> BuscarPorCategoria(int categoriaId);
        Task<Produto> Cadastrar(string nome, string descricao, double preco, int categoriaId);
        void Atualizar(int produtoId, string nome, string descricao, double preco, int categoriaId);
        void Deletar(int produtoId);
    }
}
