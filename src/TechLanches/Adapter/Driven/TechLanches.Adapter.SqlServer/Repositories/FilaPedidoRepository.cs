using Microsoft.EntityFrameworkCore;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Core;

namespace TechLanches.Adapter.SqlServer.Repositories
{
    public class FilaPedidoRepository : IFilaPedidoRepository
    {
        private readonly TechLanchesDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public FilaPedidoRepository(TechLanchesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<FilaPedido>> RetornarFilaPedidos()
        {
            return await _context.FilaPedidos
                .OrderBy(x => x.PedidoId)
                .ToListAsync();
        }
    }
}
