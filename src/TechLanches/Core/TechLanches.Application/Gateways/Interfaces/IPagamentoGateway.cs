using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Gateways.Interfaces
{
    public interface IPagamentoGateway : IRepositoryGateway
    {
        Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId);
        Task<Pagamento> Cadastrar(Pagamento pagamento);
        Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago);
        Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial);
    }
}
