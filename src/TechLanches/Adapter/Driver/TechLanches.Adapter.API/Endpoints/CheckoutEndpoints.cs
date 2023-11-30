using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.ValueObjects;
using Newtonsoft.Json;
using TechLanches.Adapter.ACL.Pagamento.QrCode;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class CheckoutEndpoints
    {
        public static void MapCheckoutEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/checkout", Checkout)
               .WithTags(EndpointTagConstantes.TAG_CHECKOUT)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Realizar checkout do pedido", description: "Retorna o checkout do pedido"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(CheckoutResponseDTO), description: "Checkout realizado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Falha ao realizar o checkout"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> Checkout(
            int pedidoId,
            [FromServices] ICheckoutService checkoutService, 
                           IPagamentoQrCodeACLService pagamentoQrCodeACLService, 
                           IPedidoService pedidoService)
        {
            var resultadoCheckout = await checkoutService.ValidaPedido(pedidoId);

            if (resultadoCheckout.Item1 is true)
            {
                var pedido = await pedidoService.BuscarPorId(pedidoId);

                var pedidoAcl = new PedidoACLDTO()
                {
                    Valor = pedido.Valor,
                    ItensPedido = pedido.ItensPedido.Adapt<List<ItemPedidoACLDTO>>()
                };

                var qrcode = await pagamentoQrCodeACLService.GerarQrCode(pedidoAcl);
                resultadoCheckout = Tuple.Create(true, qrcode);
            }

            return resultadoCheckout is not null && resultadoCheckout.Item1 is true
                   ? Results.Ok(new CheckoutResponseDTO()
                   {
                       PedidoId = pedidoId,
                       QRCodeData = resultadoCheckout.Item2
                   })
                   : Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Falha ao realizar checkout. {resultadoCheckout?.Item2}", StatusCode = (int)HttpStatusCode.BadRequest});
        }
    }
}
