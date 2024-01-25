using Mapster;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Application.DTOs;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Presenters
{

    public class CheckoutPresenter : ICheckoutPresenter
    {
        public CheckoutResponseDTO ParaDto(int pedidoId, string qrdCodeData, byte[] bytesQrCode)
        {
            return new CheckoutResponseDTO()
            {
                PedidoId = pedidoId,
                QRCodeData = qrdCodeData,
                URLData = $"https://api.qrserver.com/v1/create-qr-code/?size=1500x1500&data={qrdCodeData.Replace(" ", "%20")}",
                QRCodeImage = bytesQrCode,
                ResultType = bytesQrCode.Length > 0 ? "image/png" : String.Empty,
            };
        }

        public PedidoACLDTO ParaPedidoACLDto(Pedido pedido)
        {
            var pedidoMercadoPago = new PedidoACLDTO()
            {
                ReferenciaExterna = pedido.Id.ToString(),
                TotalTransacao = pedido.Valor,
                ItensPedido = new List<ItemPedidoACLDTO>(),
                Titulo = "Compra em TechLanches",
                Descricao = "Compra e Retirada de produto",
                DataExpiracao = DateTime.Now.AddHours(1).ToString("yyyy-MM-ddTHH:mm:ss.fffzzz"),
                UrlNotificacao = "https://spider-tight-previously.ngrok-free.app/api/pagamentos/webhook/mercadopago"
            };

            foreach (var item in pedido.ItensPedido)
            {
                var itemPedido = new ItemPedidoACLDTO()
                {
                    Categoria = CategoriaProduto.From(item.Produto.Categoria.Id).Nome,
                    NomeProduto = item.Produto.Nome,
                    Descricao = item.Produto.Descricao,
                    Quantidade = item.Quantidade,
                    UnidadeMedida = "unit",
                    PrecoProduto = item.Produto.Preco,
                    TotalTransacao = item.Valor
                };

                pedidoMercadoPago.ItensPedido.Add(itemPedido);
            }

            return pedidoMercadoPago;
        }
    }
}
