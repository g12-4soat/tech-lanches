using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode
{
    public interface IPagamentoQrCodeACLService
    {
        Task<string> GerarQrCode(string url);
        Task<PagamentoResponseACLDTO> ConsultarPagamento(int ProvedorVendaId);
    }
}