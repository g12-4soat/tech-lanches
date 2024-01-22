using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Application.DTOs;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters
{
    public class PagamentoPresenter : IPagamentoPresenter
    {
        public PagamentoResponseDTO ParaDto(Pagamento pagamento)
        {
            return pagamento.Adapt<PagamentoResponseDTO>();
        }
    }
}
