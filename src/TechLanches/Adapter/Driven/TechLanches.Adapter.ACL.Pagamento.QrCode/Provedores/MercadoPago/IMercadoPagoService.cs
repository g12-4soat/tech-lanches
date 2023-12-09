using TechLanches.Adapter.ACL.Pagamento.QrCode.Models;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public interface IMercadoPagoService
    {
        Task<PedidoComercial> ObterPedido(string pedidoComercial);
    }
}
