using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago
{
    public interface IPagamentoACLService
    {
        Task<string> GerarPagamentoEQrCode(string pagamentoMercadoPago, string usuarioId, string posId);
        Task<PagamentoResponseACLDTO> ConsultarPagamento(string idPagamentoComercial);
    }
}