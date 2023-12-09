using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode
{
    public interface IPagamentoQrCodeACLService
    {
        Task<string> GerarQrCode();
        Task<PagamentoResponseACLDTO> ConsultarPagamento(int ProvedorVendaId);
    }
}