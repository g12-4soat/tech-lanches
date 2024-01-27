using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters.Interfaces
{
    public interface ICheckoutPresenter
    {
        CheckoutResponseDTO ParaDto(int pedidoId, string qrdCodeData, byte[] bytesQrCode);
        PedidoACLDTO ParaPedidoACLDto(Pedido pedido);
    }
}
