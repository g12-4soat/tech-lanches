using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Services;

namespace TechLanches.Application
{
    public class ProdutoService : IProdutoService
    {
        public void Atualizar(int produtoId, string nome, string descricao, double preco, int categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Produto>> BuscarPorCategoria(int categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<Produto> BuscarPorId(int produtoId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Produto>> BuscarTodos()
        {
            throw new NotImplementedException();
        }

        public Task<Produto> Cadastrar(string nome, string descricao, double preco, int categoriaId)
        {
            throw new NotImplementedException();
        }
        public void Deletar(int produtoId)
        {
            throw new NotImplementedException();
        }
    }
}
