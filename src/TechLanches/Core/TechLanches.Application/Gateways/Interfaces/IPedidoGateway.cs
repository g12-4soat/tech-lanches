using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Gateways.Interfaces
{
    public interface IPedidoGateway : IRepositoryGateway
    {
        Task<List<Pedido>> BuscarTodos();
        Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido);
        Task<Pedido> BuscarPorId(int idPedido);
        Task<Pedido> Cadastrar(Pedido pedido);
    }
}
