using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class CheckoutEndpoints
    {
        public static void MapCheckoutEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/checkout", Checkout)
               .WithTags(EndpointTagConstantes.TAG_CHECKOUT)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Realizar checkout do pedido", description: "Retorna o checkout do pedido"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(CheckoutResponseDTO), description: "Checkout realizado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Falha ao realizar o checkout"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        public static async Task<IResult> Checkout(
            int pedidoId,
            [FromServices] ICheckoutService checkoutService, 
                           IPagamentoACLService pagamentoQrCodeACLService, 
                           IPedidoService pedidoService)
        {

            var checkout = await checkoutService.ValidarCheckout(pedidoId);

            string qrdCodeData = string.Empty;

            if (checkout is true)
                qrdCodeData = await checkoutService.CriarPagamentoCheckout(pedidoId);

            return checkout is true
                    ? Results.Ok(new CheckoutResponseDTO()
                    {
                        PedidoId = pedidoId,
                        QRCodeData = qrdCodeData,
                        URLData = $"https://api.qrserver.com/v1/create-qr-code/?size=1500x1500&data={qrdCodeData.Replace(" ", "%20")}"
                    })
                    : Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Falha ao realizar checkout.", StatusCode = (int)HttpStatusCode.BadRequest });
        }                                
    }
}
