using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Pagamentos;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Controllers
{
    public class CheckoutController : ICheckoutController
    {
        private readonly IPagamentoController _pagamentoController;
        private readonly IPedidoController _pedidoController;
        private readonly ICheckoutPresenter _checkoutPresenter;
        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;

        public CheckoutController(IPagamentoController pagamentoController,
                                  IPedidoController pedidoController,
                                  ICheckoutPresenter checkoutPresenter,
                                  IQrCodeGeneratorService qrCodeGeneratorService)
        {
            _pagamentoController = pagamentoController;
            _pedidoController = pedidoController;
            _checkoutPresenter = checkoutPresenter;
            _qrCodeGeneratorService = qrCodeGeneratorService;
        }

        public async Task<bool> ValidarCheckout(int pedidoId)
            => await CheckoutUseCase.ValidarPedidoCompleto(pedidoId, _pedidoController);

        public async Task<CheckoutResponseDTO> CriarPagamentoCheckout(int pedidoId, bool getImage = false)
        {
            var pedido = await _pedidoController.BuscarPorId(pedidoId);

            var pedidoMercadoPago = _checkoutPresenter.ParaPedidoACLDto(pedido);

            var qrCode = await _pagamentoController.GerarPagamentoEQrCodeMercadoPago(pedidoMercadoPago);

            await _pagamentoController.Cadastrar(pedidoId, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            var bytesQrCode = new byte[] { };

            if (getImage) bytesQrCode = _qrCodeGeneratorService.GenerateByteArray(qrCode);

            return _checkoutPresenter.ParaDto(pedidoId, qrCode, bytesQrCode);
        }
    }
}
