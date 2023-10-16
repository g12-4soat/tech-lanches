using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly TechLanchesDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public PedidoRepository(TechLanchesDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Pedido>> BuscarTodosPedidos()
    {
        return await _context.Pedidos.AsNoTracking().ToListAsync();
    }

    public async Task<Pedido> BuscarPedidoPorId(int idPedido)
    {
        return await _context.Pedidos.AsNoTracking().SingleOrDefaultAsync(x => x.Id == idPedido);
    }

    public async Task<List<Pedido>> BuscarPedidosPorStatus(StatusPedido statusPedido)
    {
        return await _context.Pedidos.AsNoTracking().Where(x => x.StatusPedido.Id == statusPedido.Id).ToListAsync();
    }

    public async Task<Pedido> Cadastrar(Pedido pedido)
    {
        return (await _context.AddAsync(pedido)).Entity;
    }
}