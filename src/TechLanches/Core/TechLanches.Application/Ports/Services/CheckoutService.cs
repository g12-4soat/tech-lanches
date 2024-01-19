using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IPedidoService _pedidoService;
        private readonly IPagamentoService _pagamentoService;
        private readonly IQrCodeGeneratorService _qrCodeGeneratorService;

        public CheckoutService(IPedidoService pedidoService, 
                               IPagamentoService pagamentoService,
                               IQrCodeGeneratorService qrCodeGeneratorService)
        {
            _pedidoService = pedidoService;
            _pagamentoService = pagamentoService;
            _qrCodeGeneratorService = qrCodeGeneratorService;   
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

            await _pagamentoService.Cadastrar(pedidoId, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            var pedidoMercadoPago = new PedidoACLDTO()
            {
                ReferenciaExterna = pedido.Id.ToString(), 
                TotalTransacao = pedido.Valor,
                ItensPedido = new List<ItemPedidoACLDTO>(),
                Titulo = "Compra em TechLanches",
                Descricao = "Compra e Retirada de produto",
                DataExpiracao = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"), 
                UrlNotificacao = "https://spider-tight-previously.ngrok-free.app/api/pagamentos/webhook/mercadopago" //alterar para endpoint de pagamento para receber notificacao
            };

            foreach(var item in pedido.ItensPedido)
            {
                var itemPedido = new ItemPedidoACLDTO()
                {
                    Categoria = CategoriaProduto.From(item.Produto.Categoria.Id).Nome,
                    NomeProduto = item.Produto.Nome,
                    Descricao = item.Produto.Descricao,
                    Quantidade = item.Quantidade,
                    UnidadeMedida = "unit",
                    PrecoProduto = item.PrecoProduto,
                    TotalTransacao = item.Valor
                };

                pedidoMercadoPago.ItensPedido.Add(itemPedido);
            }

            var qrCode = await _pagamentoService.GerarPagamentoEQrCodeMercadoPago(pedidoMercadoPago);

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
