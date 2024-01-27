using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters.Interfaces
{
    public interface IProdutoPresenter
    {
        ProdutoResponseDTO ParaDto(Produto entidade);
        List<ProdutoResponseDTO> ParaListaDto(List<Produto> entidade);
    }
}
