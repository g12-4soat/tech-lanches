using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IPedidoController _pedidoController;
        private readonly IPagamentoService _pagamentoService;
        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;
        private readonly IPedidoPresenter _pedidoPresenter;
        public CheckoutService(IPedidoController pedidoController,
                               IPagamentoService pagamentoService,
                               IQrCodeGeneratorService qrCodeGeneratorService,
                               IPedidoPresenter pedidoPresenter)
        {
            _pedidoController = pedidoController;
            _pagamentoService = pagamentoService;
            _qrCodeGeneratorService = qrCodeGeneratorService;
            _pedidoPresenter = pedidoPresenter;
        }

        public async Task<bool> ValidarCheckout(int pedidoId)
        {
            var pedido = await _pedidoController.BuscarPorId(pedidoId)
                ?? throw new DomainException($"Pedido não encontrado para checkout - PedidoId: {pedidoId}");

            if (pedido.StatusPedido != StatusPedido.PedidoCriado)
                throw new DomainException($"Status não autorizado para checkout - StatusPedido: {pedido.NomeStatusPedido}");

            if (pedido.Pagamentos is not null)
                throw new DomainException($"Pedido já contém pagamento - StatusPagamento: {pedido.Pagamentos?.FirstOrDefault()?.StatusPagamento}");

            return true;
        }

        public async Task<string> CriarPagamentoCheckout(int pedidoId)
        {
            var pedido = await _pedidoController.BuscarPorId(pedidoId);

            var pedidoMercadoPago = _pedidoPresenter.ParaAclDto(pedido);

            var qrCode = await _pagamentoService.GerarPagamentoEQrCodeMercadoPago(pedidoMercadoPago);

            await _pagamentoService.Cadastrar(pedidoId, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            return qrCode;
        }

        public async Task<byte[]> CriarPagamentoEQrCode(int pedidoId)
        {
            var qrCode = await CriarPagamentoCheckout(pedidoId);

            var bytesQrCode = _qrCodeGeneratorService.GenerateByteArray(qrCode);

            return bytesQrCode;
        }
    }
}
