using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Presenters.Interfaces
{
    public interface IPagamentoPresenter
    {
        PagamentoResponseDTO ParaDto(Pagamento pagamento);
    }
}
