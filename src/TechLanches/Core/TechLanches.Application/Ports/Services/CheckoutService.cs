using Mapster;
using System.Drawing;
using TechLanches.Adapter.ACL.Pagamento.QrCode;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPagamentoService _pagamentoService;
        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;
        private readonly IPagamentoQrCodeACLService _pagamentoQrCodeACLService;

        public CheckoutService(IPedidoService pedidoService, 
                               IPagamentoService pagamentoService,
                               IQrCodeGeneratorService qrCodeGeneratorService,
                               IPagamentoQrCodeACLService pagamentoQrCodeACLService)
        {
            _pedidoService = pedidoService;
            _pagamentoService = pagamentoService;
            _qrCodeGeneratorService = qrCodeGeneratorService;   
            _pagamentoQrCodeACLService = pagamentoQrCodeACLService;
        }

        public async Task<bool> ValidarCheckout(int pedidoId)
        {
            var pedido = await _pedidoService.BuscarPorId(pedidoId)
                ?? throw new DomainException($"Pedido não encontrado para checkout - PedidoId: {pedidoId}");

            if (pedido.StatusPedido != StatusPedido.PedidoCriado)
                throw new DomainException($"Status não autorizado para checkout - StatusPedido: {pedido.StatusPedido}");

            if(pedido.Pagamentos is not null)
                throw new DomainException($"Pedido já contém pagamento - StatusPagamento: {pedido.Pagamentos?.FirstOrDefault()?.StatusPagamento}");

            return true;
        }

        public async Task<string> CriarPagamentoCheckout(int pedidoId)
        {
            var pedido = await _pedidoService.BuscarPorId(pedidoId);

            //pedido = await _pagamentoService.RealizarPagamento(pedidoId, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            var pedidoAcl = new PedidoACLDTO()
            {
                Valor = pedido.Valor,
                ItensPedido = pedido.ItensPedido.Adapt<List<ItemPedidoACLDTO>>()
            };

            var qrCode = await _pagamentoQrCodeACLService.GerarQrCode(pedidoAcl);

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
