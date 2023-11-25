using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<List<string>> ValidaPedido(int pedidoId);
    }
}