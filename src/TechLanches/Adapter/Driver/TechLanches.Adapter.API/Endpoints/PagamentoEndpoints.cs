using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class PagamentoEndpoints
    {
        public static void MapPagamentoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/pagamentos/status/{pedidoId}", BuscarStatusPagamentoPorPedidoId).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Buscar status pagamento", description: "Retorna o status do pagamento"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(PagamentoStatusResponseDTO), description: "Status do pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Pedido não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno")); ;

            app.MapPost("api/pagamentos/webhook/mercadopago", BuscarPagamento).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Webhook pagamento do mercado pago", description: "Retorna o pagamento"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, description: "Pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Pagamento não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapGet("api/pagamentos/webhook/mockado", BuscarPagamentoMockado).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Webhook pagamento mockado", description: "Retorna o pagamento"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, description: "Pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Pagamento não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> BuscarStatusPagamentoPorPedidoId([FromRoute] int pedidoId, [FromServices] IPagamentoService pagamentoService)
        {
            var pagamento = await pagamentoService.BuscarPagamentoPorPedidoId(pedidoId);

            if (pagamento is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {pedidoId}", StatusCode = HttpStatusCode.NotFound });

            return Results.Ok(pagamento.Adapt<PagamentoStatusResponseDTO>());
        }

        private static async Task<IResult> BuscarPagamento(int id, TopicACL topic, [FromServices] IPagamentoService pagamentoService, [FromServices] IPedidoService pedidoService, [FromServices] IRabbitMqService rabbitmqService)
        {
            if (topic == TopicACL.merchant_order)
            {
                var pagamentoExistente = await pagamentoService.ConsultarPagamentoMercadoPago(id.ToString());

                if (pagamentoExistente is null)
                    return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {id}", StatusCode = HttpStatusCode.NotFound });

                var pagamento = await pagamentoService.RealizarPagamento(pagamentoExistente.PedidoId, pagamentoExistente.StatusPagamento);

                if (pagamento)
                {
                    await pedidoService.TrocarStatus(pagamentoExistente.PedidoId, StatusPedido.PedidoRecebido);
                    rabbitmqService.Publicar(pagamentoExistente.PedidoId);
                }
                else
                    await pedidoService.TrocarStatus(pagamentoExistente.PedidoId, StatusPedido.PedidoCancelado);
            }
            return Results.Ok();
        }

        private static async Task<IResult> BuscarPagamentoMockado(int pedidoId, [FromServices] IPagamentoService pagamentoService, [FromServices] IPedidoService pedidoService, [FromServices] IRabbitMqService rabbitmqService)
        {
            var pagamentoExistente = await pagamentoService.ConsultarPagamentoMockado(pedidoId.ToString());

            var pagamento = await pagamentoService.RealizarPagamento(pedidoId, pagamentoExistente.StatusPagamento);

            if (pagamento)
            {
                await pedidoService.TrocarStatus(pedidoId, StatusPedido.PedidoRecebido);
                rabbitmqService.Publicar(pedidoId);
                return Results.Ok();
            }
            else
            {
                await pedidoService.TrocarStatus(pedidoId, StatusPedido.PedidoCancelado);
                return Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Erro ao realizar o pagamento.", StatusCode = HttpStatusCode.NotFound });
            }

        }
    }
}
