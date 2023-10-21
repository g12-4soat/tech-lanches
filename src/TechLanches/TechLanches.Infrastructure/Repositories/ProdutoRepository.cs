using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.ValueObjects;
using TechLanches.Infrastructure.Migrations;

namespace TechLanches.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly TechLanchesDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public ProdutoRepository(TechLanchesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Atualizar(Produto produto)
        {
            _context.Entry(produto).State = EntityState.Modified;
        }

        public async Task<List<Produto>> BuscarPorCategoria(CategoriaProduto categoria)
        {
            return await _context.Produtos.Where(x => x.Categoria.Id == categoria.Id).ToListAsync(); //mesmo erro do Cliente?
        }

        public async Task<Produto> BuscarPorId(int produtoId)
        {
            return await _context.Produtos.SingleOrDefaultAsync(x => x.Id == produtoId);
        }

        public async Task<Produto> BuscarPorNome(string nome)
        {
            return await _context.Produtos.SingleOrDefaultAsync(x => x.Nome == nome);
        }

        public async Task<List<Produto>> BuscarTodos()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task<Produto> Cadastrar(Produto produto)
        {
            return (await _context.AddAsync(produto)).Entity;
        }

        public async void Deletar(Produto produto)
        {
            produto.Deletado = true;
        }       
    }
}
