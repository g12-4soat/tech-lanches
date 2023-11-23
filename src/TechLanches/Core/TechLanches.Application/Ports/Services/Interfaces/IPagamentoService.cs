using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Ports.Services.Interfaces;

public interface IPagamentoService
{
    Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor);
    Task<Pagamento> BuscarStatusPagamentoPorPedidoId(int pedidoId);
}

public enum FormaPagamento
{
    QrCodeMercadoPago
}