using Mapster;
using TechLanches.Adapter.ACL.Pagamento.QrCode;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPagamentoService _pagamentoService;
        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;
        private readonly IPagamentoQrCodeACLService _pagamentoQrCodeACLService;
        private readonly IMercadoPagoService _mercadoPagoService;

        public CheckoutService(IPedidoService pedidoService, 
                               IPagamentoService pagamentoService,
                               IQrCodeGeneratorService qrCodeGeneratorService,
                               IMercadoPagoService mercadoPagoService,
                               IPagamentoQrCodeACLService pagamentoQrCodeACLService)
        {
            _pedidoService = pedidoService;
            _pagamentoService = pagamentoService;
            _qrCodeGeneratorService = qrCodeGeneratorService;   
            _pagamentoQrCodeACLService = pagamentoQrCodeACLService;
            _mercadoPagoService = mercadoPagoService;
        }

        public async Task<bool> ValidarCheckout(int pedidoId)
        {
            var pedido = await _pedidoService.BuscarPorId(pedidoId)
                ?? throw new DomainException($"Pedido não encontrado para checkout - PedidoId: {pedidoId}");

            if (pedido.StatusPedido != StatusPedido.PedidoCriado)
                throw new DomainException($"Status não autorizado para checkout - StatusPedido: {pedido.StatusPedido}");

            if(pedido.Pagamentos is not null)
                throw new DomainException($"Pedido já contém pagamento - StatusPagamento: {pedido.Pagamentos?.FirstOrDefault()?.StatusPagamento}");

            //var teste = await _mercadoPagoService.ObterPedido("1d500ba2-ae69-442f-9f30-ccf22955b11f");

            return true;
        }

        public async Task<string> CriarPagamentoCheckout(int pedidoId)
        {
            var pedido = await _pedidoService.BuscarPorId(pedidoId);

            await _pagamentoService.Cadastrar(pedidoId, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            #region
            //var pedidoAcl = new PedidoACLDTO()
            //{
            //    Valor = pedido.Valor,
            //    ItensPedido = pedido.ItensPedido.Adapt<List<ItemPedidoACLDTO>>()
            //};

            //var qrCode = await _pagamentoQrCodeACLService.GerarQrCode(pedidoAcl);
            #endregion

            var qrCode = await _pagamentoService.GerarQrCode();
            
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
