using Microsoft.EntityFrameworkCore;
using TechLanches.Adapter.SqlServer;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Repositories;

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
        => await _context.Pedidos.Include(x => x.ItensPedido).ThenInclude(i => i.Produto).Include(x => x.Cliente).ToListAsync();

    public async Task<Pedido> BuscarPorId(int idPedido)
        => await _context.Pedidos.Include(x => x.ItensPedido).ThenInclude(i => i.Produto).Include(x => x.Cliente).SingleOrDefaultAsync(x => x.Id == idPedido);

    public async Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
        => await _context.Pedidos.Include(x => x.ItensPedido).ThenInclude(i => i.Produto).Include(x => x.Cliente).Where(x => x.StatusPedido == statusPedido).ToListAsync();

    public async Task<Pedido> Cadastrar(Pedido pedido)
        => (await _context.AddAsync(pedido)).Entity;

    public void Atualizar(Pedido pedido)
        => _context.Entry(pedido).State = EntityState.Modified;
}