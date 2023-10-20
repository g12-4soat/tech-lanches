using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Repositories;

namespace TechLanches.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly TechLanchesDbContext _context;

    public IUnitOfWork UnitOfWork => _context;

    public PedidoRepository(TechLanchesDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<Pedido>> BuscarTodos()
    {
        var query  = (from p in _context.Pedidos
                     join i in _context.ItemPedido
                     on p.Id equals i.PedidoId
                     orderby p.Id ascending
                     select p).Distinct();

        return await query.AsNoTracking().ToListAsync();
    }

    public async Task<Pedido> BuscarPorId(int idPedido)
    {
        return await _context.Pedidos.AsNoTracking().SingleOrDefaultAsync(x => x.Id == idPedido);
    }

    public async Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
    {
        return await _context.Pedidos.AsNoTracking().Where(x => x.StatusPedido == statusPedido).ToListAsync();
    }

    public async Task<Pedido> Cadastrar(Pedido pedido)
    {
        return (await _context.AddAsync(pedido)).Entity;
    }

    public void Atualizar(Pedido pedido)
    {
        _context.Entry(pedido).State = EntityState.Modified;
    }
}