using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IPagamentoRepository : IRepository<Pagamento>
    {
        Task<Pagamento> BuscarStatusPagamentoPorPedidoId(int pedidoId);
        Task<Pagamento> Cadastrar(Pagamento pagamento);
        void Aprovar(Pagamento pagamento);
        void Reprovar(Pagamento pagamento);
    }
}
