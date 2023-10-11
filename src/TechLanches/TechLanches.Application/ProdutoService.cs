using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Services;

namespace TechLanches.Application
{
    public class ProdutoService : IProdutoService
    {
        public Task<Produto> Atualizar(int produtoId)
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

        public Task<Produto> Cadastrar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public Task Deletar(int produtoId)
        {
            throw new NotImplementedException();
        }
    }
}
