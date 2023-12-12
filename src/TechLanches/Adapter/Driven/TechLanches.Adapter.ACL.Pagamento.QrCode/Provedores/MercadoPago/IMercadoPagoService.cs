using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Models;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public interface IMercadoPagoService
    {
        //Task<PedidoComercial> ObterPedido(string pedidoComercial);
        //Task<PedidoGerado> GerarPedido(string pedidoMercadoPago, string usuarioId, string posId);

        Task<PagamentoResponseACLDTO> ConsultarPagamento(string pedidoComercial);
        Task<string> GerarPedidoEQrCode(string pedidoMercadoPago, string usuarioId, string posId);
    }
}
