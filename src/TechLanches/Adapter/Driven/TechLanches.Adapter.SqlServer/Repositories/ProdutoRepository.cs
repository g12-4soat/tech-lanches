using Microsoft.EntityFrameworkCore;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.SqlServer.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly TechLanchesDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public ProdutoRepository(TechLanchesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<Produto>> BuscarPorCategoria(CategoriaProduto categoria)
        {
            return await _context.Produtos.Where(x => x.Categoria.Id == categoria.Id).ToListAsync();
        }

        public async Task<Produto> BuscarPorId(int produtoId)
        {
            return await _context.Produtos.SingleOrDefaultAsync(x => x.Id == produtoId);
        }

        public async Task<Produto> BuscarPorNome(string nomeProduto)
            => await _context.Produtos.SingleOrDefaultAsync(x => x.Nome.ToLower().Trim() == nomeProduto.ToLower().Trim());

        public async Task<List<Produto>> BuscarTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> Cadastrar(Produto produto)
        {
            return (await _context.AddAsync(produto)).Entity;
        }
    }
}
