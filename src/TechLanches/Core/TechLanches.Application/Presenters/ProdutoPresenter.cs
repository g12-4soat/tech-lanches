﻿using Mapster;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters
{
    public interface IProdutoPresenter
    {
        ProdutoResponseDTO ParaDto(Produto entidade);
        List<ProdutoResponseDTO> ParaListaDto(List<Produto> entidade);
    }

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
