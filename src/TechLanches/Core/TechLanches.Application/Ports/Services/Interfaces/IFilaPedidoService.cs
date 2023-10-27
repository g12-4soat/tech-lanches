using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface IFilaPedidoService
    {
        Task<Pedido?> RetornarPrimeiroPedidoDaFila();
        Task TrocarStatus(Pedido pedido, StatusPedido statusPedido);
    }
}