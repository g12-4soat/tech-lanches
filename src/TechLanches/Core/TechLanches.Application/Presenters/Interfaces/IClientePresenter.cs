using TechLanches.Application.DTOs;
using TechLanches.Domain.Entities;

namespace TechLanches.Application.Presenters.Interfaces
{
    public interface IClientePresenter
    {
        ClienteResponseDTO ParaDto(Cliente entidade);
    }
}