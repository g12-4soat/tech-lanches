using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.ValueObjects;
using Newtonsoft.Json;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class CheckoutEndpoints
    {
        public static void MapCheckoutEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/checkout", Checkout)
               .WithTags(EndpointTagConstantes.TAG_CHECKOUT)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Realizar checkout do pedido", description: "Retorna o checkout do pedido"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(ProdutoResponseDTO), description: "Checkout realizado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Falha ao realizar o checkout"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> Checkout(
            int idPedido,
            [FromServices] ICheckoutService checkoutService)
        {
            var checkout = await checkoutService.ValidaPedido(idPedido);

            if (checkout.Count == 0) checkout.Add("Checkout realizado com sucesso!");

            //verificar se vai implementar camada de pagamento/ RealizarPagamento QRCode

            return checkout is not null
                   ? Results.Ok(JsonConvert.SerializeObject(checkout))
                   : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao realizar checkout.", StatusCode = (int)HttpStatusCode.BadRequest});
        }
    }
}
