using TechLanches.Application.Controllers.Interfaces;

namespace TechLanches.Application.Controllers
{
    public class CheckoutController : ICheckoutController
    {
        private readonly IPagamentoController _pagamentoController;
        //private readonly IPedidoController _pedidoController;

        public CheckoutController(IPagamentoController pagamentoController
                                  /*IPedidoController pedidoController*/)
        {
            _pagamentoController = pagamentoController;
            //_pedidoController = pedidoController;
        }

        //public async Task<bool> ValidarCheckout(int pedidoId)
        //    => await CheckoutUseCase.ValidarPedidoCompleto(int pedidoId, IPedidoController);

        //public async Task<string> CriarPagamentoCheckout(int pedidoId)
        //{
        //    var pedido = await _pedidoController.BuscarPorId(pedidoId);

        //    var pedidoMercadoPago = new PedidoACLDTO()
        //    {
        //        ReferenciaExterna = pedido.Id.ToString(),
        //        TotalTransacao = pedido.Valor,
        //        ItensPedido = new List<ItemPedidoACLDTO>(),
        //        Titulo = "Compra em TechLanches",
        //        Descricao = "Compra e Retirada de produto",
        //        DataExpiracao = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
        //        UrlNotificacao = "https://spider-tight-previously.ngrok-free.app/api/pagamentos/webhook/mercadopago" 
        //    };

        //    foreach (var item in pedido.ItensPedido)
        //    {
        //        var itemPedido = new ItemPedidoACLDTO()
        //        {
        //            Categoria = CategoriaProduto.From(item.Produto.Categoria.Id).Nome,
        //            NomeProduto = item.Produto.Nome,
        //            Descricao = item.Produto.Descricao,
        //            Quantidade = item.Quantidade,
        //            UnidadeMedida = "unit",
        //            PrecoProduto = item.PrecoProduto,
        //            TotalTransacao = item.Valor
        //        };

        //        pedidoMercadoPago.ItensPedido.Add(itemPedido);
        //    }

        //    var qrCode = await _pagamentoController.GerarPagamentoEQrCodeMercadoPago(pedidoMercadoPago);

        //    await _pagamentoController.Cadastrar(pedidoId, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

        //    return qrCode;
        //}

        //public async Task<byte[]> CriarPagamentoEQrCode(int pedidoId)
        //{
        //    var qrCode = await CriarPagamentoCheckout(pedidoId);

        //    var bytesQrCode = _qrCodeGeneratorService.GenerateByteArray(qrCode);

        //    return bytesQrCode;
        //}
    }
}
