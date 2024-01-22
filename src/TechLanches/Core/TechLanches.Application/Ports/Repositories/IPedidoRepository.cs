using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido>
    {
        Task<List<Pedido>> BuscarTodos();
        Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido);
        Task<Pedido> BuscarPorId(int idPedido);
        Task<Pedido> Cadastrar(Pedido pedido);
    }
}