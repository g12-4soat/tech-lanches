using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Adapter.SqlServer.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly TechLanchesDbContext _context;

        public IUnitOfWork UnitOfWork => _context;

        public PagamentoRepository(TechLanchesDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Pagamento> BuscarStatusPagamentoPorPedidoId(int pedidoId)
        {
            return await _context.Pagamentos.SingleOrDefaultAsync(x => x.PedidoId == pedidoId);
        }
    }
}
