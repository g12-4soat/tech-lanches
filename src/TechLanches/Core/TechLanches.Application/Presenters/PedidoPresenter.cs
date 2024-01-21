using Mapster;
using TechLanches.Application.DTOs;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters
{
    public class PedidoPresenter : IPedidoPresenter
    {
        public PedidoResponseDTO ParaDto(Pedido entidade)
        {
            return entidade.Adapt<PedidoResponseDTO>();
        }

        public List<PedidoResponseDTO> ParaListaDto(List<Pedido> entidade)
        {
            return entidade.Adapt<List<PedidoResponseDTO>>();
        }
    }
}
