using TechLanches.Application.DTOs;

namespace TechLanches.Application.Gateways.Interfaces
{
    public interface IFilaPedidoGateway
    {
        Task<List<FilaPedido>> RetornarFilaPedidos();
    }
}
