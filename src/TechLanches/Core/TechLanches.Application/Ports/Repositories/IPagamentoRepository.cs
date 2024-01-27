using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId);
        Task<Pagamento> Cadastrar(Pagamento pagamento);
    }
}
