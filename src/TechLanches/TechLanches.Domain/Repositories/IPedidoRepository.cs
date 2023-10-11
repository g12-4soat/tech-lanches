using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;

namespace TechLanches.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<List<Pedido>> BuscarTodosPedidos();
        Task<Pedido> BuscarPedidoPorId(int idPedido);
    }
}