using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<List<Pedido>> BuscarTodosPedidos();
        Task<List<Pedido>> BuscarPedidosPorStatus(StatusPedido statusPedido);
        Task<Pedido> BuscarPedidoPorId(int idPedido);
        Task<Pedido> CadastrarPedido(Pedido pedido);
    }
}