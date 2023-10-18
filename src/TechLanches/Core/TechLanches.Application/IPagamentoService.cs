namespace TechLanches.Application;

public interface IPagamentoService
{
    Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor);
}

public class FakeCheckoutService : IPagamentoService
{
    public Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor)
    {
        //salvar informação de pagamento
        return Task.FromResult(true);
    }
}

public enum FormaPagamento
{
    QrCodeMercadoPago
}