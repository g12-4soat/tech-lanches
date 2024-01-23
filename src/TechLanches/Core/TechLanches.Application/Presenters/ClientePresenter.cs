using Mapster;
using TechLanches.Application.DTOs;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Entities;

namespace TechLanches.Application.Presenters
{
    public class ClientePresenter : IClientePresenter
    {
        public ClienteResponseDTO ParaDto(Cliente entidade)
        {
            return entidade.Adapt<ClienteResponseDTO>();
        }
    }
}