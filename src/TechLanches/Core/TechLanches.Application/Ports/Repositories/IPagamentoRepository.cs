using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento> BuscarStatusPagamentoPorPedidoId(int pedidoId);
    }
}
