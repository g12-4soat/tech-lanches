using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services.Interfaces;

public interface IPagamentoService
{
    Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor);
    Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId);
    Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor);
    Task Aprovar(Pagamento pagamento);
    Task Reprovar(Pagamento pagamento);
    Task<string> GerarQrCode();
}
