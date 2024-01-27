using Microsoft.EntityFrameworkCore;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.SqlServer.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly TechLanchesDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public PedidoRepository(TechLanchesDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Pedido>> BuscarTodos()
       => await _context.Pedidos.Include(x => x.ItensPedido)
                                .ThenInclude(i => i.Produto)
                                .Include(x => x.Cliente)
                                .Where(x => x.StatusPedido != StatusPedido.PedidoFinalizado)
                                .OrderBy(x => x.Id)
                                .ThenBy(x => x.StatusPedido == StatusPedido.PedidoPronto)
                                .ThenBy(x => x.StatusPedido == StatusPedido.PedidoEmPreparacao)
                                .ThenBy(x => x.StatusPedido == StatusPedido.PedidoRecebido)
                                .ToListAsync();

    public async Task<Pedido> BuscarPorId(int idPedido)
        => await _context.Pedidos.Include(x => x.ItensPedido)
                                 .ThenInclude(i => i.Produto)
                                 .Include(x => x.Cliente)
                                 .Include(x => x.Pagamentos)
                                 .SingleOrDefaultAsync(x => x.Id == idPedido);

    public async Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
        => await _context.Pedidos.Include(x => x.ItensPedido)
                                 .ThenInclude(i => i.Produto)
                                 .Include(x => x.Cliente)
                                 .Where(x => x.StatusPedido == statusPedido)
                                 .OrderBy(x => x.Id)
                                 .ToListAsync();

    public async Task<Pedido> Cadastrar(Pedido pedido)
        => (await _context.AddAsync(pedido)).Entity;
}