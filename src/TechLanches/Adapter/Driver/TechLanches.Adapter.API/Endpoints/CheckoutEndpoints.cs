using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Adapter.ACL.Pagamento.QrCode;

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

        public static async Task<IResult> Checkout(
            int pedidoId,
            [FromServices] ICheckoutService checkoutService, 
                           IPagamentoQrCodeACLService pagamentoQrCodeACLService, 
                           IPedidoService pedidoService)
        {

            var checkout = await checkoutService.ValidarCheckout(pedidoId);

#if DEBUG
            string qrdCodeData = string.Empty;

            if (checkout is true)
                qrdCodeData = await checkoutService.CriarPagamentoCheckout(pedidoId);

            return checkout is true
                  ? Results.Ok(new CheckoutResponseDTO()
                  {
                      PedidoId = pedidoId,
                      QRCodeData = qrdCodeData
                  })
                  : Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Falha ao realizar checkout.", StatusCode = (int)HttpStatusCode.BadRequest });
#else
        byte[] qrCode = new byte[] { };

        if (checkout is true)
                qrCode = await checkoutService.CriarPagamentoEQrCode(pedidoId);

            return checkout is true
                      ? Results.File(qrCode, "image/png")
                      : Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Falha ao realizar checkout.", StatusCode = (int)HttpStatusCode.BadRequest });
#endif
        }
    }
}
