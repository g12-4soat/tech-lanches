using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<Tuple<bool, string>> ValidaPedido(int pedidoId);
    }
}