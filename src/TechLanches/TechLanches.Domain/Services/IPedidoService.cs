using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;

namespace TechLanches.Domain.Services
{
    public interface IPedidoService
    {
        Task<List<Pedido>> BuscarTodosPedidos();
        Task<Pedido> BuscarPedidoPorId(int idPedido);
    }
}