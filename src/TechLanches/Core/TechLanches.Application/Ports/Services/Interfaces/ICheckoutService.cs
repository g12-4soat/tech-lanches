namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface ICheckoutService
    {
        Task<bool> ValidarCheckout(int pedidoId);
        Task<byte[]> CriarPagamentoEQrCode(int pedidoId);
        Task<string> CriarPagamentoCheckout(int pedidoId);
    }
}