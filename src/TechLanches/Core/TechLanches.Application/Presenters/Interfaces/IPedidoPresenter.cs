using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters.Interfaces
{
    public interface IPedidoPresenter
    {
        PedidoResponseDTO ParaDto(Pedido entidade);
        List<PedidoResponseDTO> ParaListaDto(List<Pedido> entidade);
        PedidoACLDTO ParaAclDto(PedidoResponseDTO pedidoDto);
    }
}
