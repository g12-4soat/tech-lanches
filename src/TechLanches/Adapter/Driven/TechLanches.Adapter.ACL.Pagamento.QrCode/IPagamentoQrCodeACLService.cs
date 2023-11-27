using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode
{
    public interface IPagamentoQrCodeACLService
    {
        Task<string> GerarQrCode(PedidoACLDTO Pedido);
        Task<PagamentoResponseACLDTO> ConsultarPagamento(int ProvedorVendaId);
    }
}