using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
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
        => new List<Pedido>() { new Pedido(1,2, new List<ItemPedido>() { new ItemPedido(2, 1, 4, 20) }) };

    public async Task<Pedido> BuscarPedidoPorId(int idPedido)
        => new Pedido(1, 2, new List<ItemPedido>() { new ItemPedido(2, 1, 4, 20) });

    //public async Task<List<Pedido>> BuscarTodosPedidos()
    //    => await _context.Pedidos.AsNoTracking().ToListAsync();

    //public async Task<Pedido> BuscarPedidoPorId(int idPedido)
    //    => await _context.Pedidos.FindAsync(idPedido);
}