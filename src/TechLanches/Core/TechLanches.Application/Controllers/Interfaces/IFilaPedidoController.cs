using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Controllers.Interfaces
{
    public interface IFilaPedidoController
    {
        Task<PedidoResponseDTO?> RetornarPrimeiroPedidoDaFila();
        Task TrocarStatus(Pedido pedido, StatusPedido statusPedido);
    }
}
