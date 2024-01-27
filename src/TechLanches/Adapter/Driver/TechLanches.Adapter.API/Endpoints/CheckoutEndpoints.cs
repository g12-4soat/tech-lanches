using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Controllers.Interfaces;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class CheckoutEndpoints
    {
        public static void MapCheckoutEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("api/checkout", Checkout)
               .WithTags(EndpointTagConstantes.TAG_CHECKOUT)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Realizar checkout do pedido", description: "Retorna o checkout do pedido"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(CheckoutResponseDTO), description: "Checkout realizado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Falha ao realizar o checkout"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        public static async Task<IResult> Checkout(
            int pedidoId,
            [FromServices] ICheckoutController checkoutController)
        {
            var checkout = await checkoutController.ValidarCheckout(pedidoId);

            var qrdCodeData = new CheckoutResponseDTO();

            if (checkout is true)
                qrdCodeData = await checkoutController.CriarPagamentoCheckout(pedidoId);

            return checkout is false ?
                   Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Falha ao realizar checkout.", StatusCode = HttpStatusCode.BadRequest }) :
                   qrdCodeData.QRCodeImage.Length > 0 ?
                   Results.File(qrdCodeData.QRCodeImage, "image/png") :
                   Results.Ok(qrdCodeData);
        }
    }
}
