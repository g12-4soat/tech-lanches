using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class PagamentoEndpoints
    {
        public static void MapPagamentoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/pagamentos/status/{pedidoId}", BuscarStatusPagamentoPorPedidoId).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Buscar status pagamento", description: "Retorna o status do pagamento"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(PagamentoResponseDTO), description: "Status do pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Pedido não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno")); ;

            app.MapPost("api/pagamentos/webhook/mercadopago", BuscarPagamento).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Webhook pagamento do mercado pago", description: "Retorna o pagamento"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, description: "Pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Pagamento não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapPost("api/pagamentos/webhook/mockado", BuscarPagamentoMockado).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Webhook pagamento mockado", description: "Retorna o pagamento"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, description: "Pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Pagamento não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> BuscarStatusPagamentoPorPedidoId([FromRoute] int pedidoId, [FromServices] IPagamentoController pagamentoController)
        {
            var pagamento = await pagamentoController.BuscarPagamentoPorPedidoId(pedidoId);

            if (pagamento is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {pedidoId}", StatusCode = HttpStatusCode.NotFound });

            return Results.Ok(pagamento);
        }


        private static async Task<IResult> BuscarPagamento(long id, TopicACL topic, [FromServices] IPagamentoController pagamentoController, [FromServices] IPedidoController pedidoController)
        {
            if (topic == TopicACL.merchant_order)
            {
                var pagamentoExistente = await pagamentoController.ConsultarPagamentoMercadoPago(id.ToString());

                if (pagamentoExistente is null)
                    return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {id}", StatusCode = HttpStatusCode.NotFound });

                var pagamento = await pagamentoController.RealizarPagamento(pagamentoExistente.PedidoId, pagamentoExistente.StatusPagamento);

                if (pagamento)
                    await pedidoController.TrocarStatus(pagamentoExistente.PedidoId, StatusPedido.PedidoRecebido);
                else
                    await pedidoController.TrocarStatus(pagamentoExistente.PedidoId, StatusPedido.PedidoCanceladoPorPagamentoRecusado);
            }
            return Results.Ok();
        }

        private static async Task<IResult> BuscarPagamentoMockado([FromBody] PagamentoMocadoRequestDTO request, [FromServices] IPagamentoController pagamentoController, [FromServices] IPedidoController pedidoController)
        {
            var pagamentoExistente = await pagamentoController.ConsultarPagamentoMockado(request.PedidoId.ToString());

            var pagamento = await pagamentoController.RealizarPagamento(request.PedidoId, pagamentoExistente.StatusPagamento);

            if (pagamento)
            {
                await pedidoController.TrocarStatus(request.PedidoId, StatusPedido.PedidoRecebido);
                return Results.Ok();
            }
            else
            {
                await pedidoController.TrocarStatus(request.PedidoId, StatusPedido.PedidoCanceladoPorPagamentoRecusado);
                return Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Pagamento recusado.", StatusCode = HttpStatusCode.BadRequest });
            }

        }
    }
}
