using Mapster;
using TechLanches.Application.DTOs;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters
{
    public class ProdutoPresenter : IProdutoPresenter
    {
        public ProdutoResponseDTO ParaDto(Produto entidade)
        {
            return entidade.Adapt<ProdutoResponseDTO>();
        }

        public List<ProdutoResponseDTO> ParaListaDto(List<Produto> entidade)
        {
            return entidade.Adapt<List<ProdutoResponseDTO>>();
        }
    }
}
