using TechLanches.Application.DTOs;

namespace TechLanches.Application.Controllers.Interfaces
{
    public interface ICheckoutController
    {
        Task<bool> ValidarCheckout(int pedidoId);
        Task<CheckoutResponseDTO> CriarPagamentoCheckout(int pedidoId, bool getImage = false);
    }
}
