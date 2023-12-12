using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class PagamentoEndpoints
    {
        public static void MapPagamentoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/pagamentos/status/{pedidoId}", BuscarStatusPagamentoPorPedidoId).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Buscar status pagamento", description: "Retorna o status do pagamento"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PagamentoStatusResponseDTO), description: "Status do pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pedido não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno")); ;

            app.MapPost("api/pagamentos", BuscarPagamento).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Webhook pagamento do mercado pago", description: "Retorna o pagamento"))
               .WithMetadata(new SwaggerResponseAttribute(200, description: "Pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pagamento não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapGet("api/pagamentos/mocado", BuscarPagamentoMocado).WithTags(EndpointTagConstantes.TAG_PAGAMENTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Webhook pagamento mocado", description: "Retorna o pagamento"))
               .WithMetadata(new SwaggerResponseAttribute(200, description: "Pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pagamento não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> BuscarStatusPagamentoPorPedidoId([FromRoute] int pedidoId, [FromServices] IPagamentoService pagamentoService)
        {
            var pagamento = await pagamentoService.BuscarPagamentoPorPedidoId(pedidoId);

            if (pagamento is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {pedidoId}", StatusCode = (int)HttpStatusCode.NotFound });

            return Results.Ok(pagamento.Adapt<PagamentoStatusResponseDTO>());
        }

        private static async Task<IResult> BuscarPagamento(int id, TopicACL topic, [FromServices] IPagamentoACLService pagamentoQrCodeACLService, [FromServices] IPagamentoService pagamentoService)
        {
            if (topic == TopicACL.merchant_order)
            {
                var pagamentoACL = await pagamentoQrCodeACLService.ConsultarPagamento(id.ToString());

                if (pagamentoACL is null)
                    return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {id}", StatusCode = (int)HttpStatusCode.NotFound });

                await pagamentoService.RealizarPagamento(pagamentoACL.PedidoId, pagamentoACL.StatusPagamento);
            }
            return Results.Ok();
        }

        private static async Task<IResult> BuscarPagamentoMocado(int pedidoId, [FromServices] IPagamentoACLService pagamentoQrCodeACLService, [FromServices] IPagamentoService pagamentoService)
        {
            var pagamentoACL = await pagamentoQrCodeACLService.ConsultarPagamento(pedidoId.ToString());

            var pagamento = await pagamentoService.RealizarPagamento(pedidoId, pagamentoACL.StatusPagamento);

            if (pagamento)
                return Results.Ok();
            else
                return Results.BadRequest(new ErrorResponseDTO { MensagemErro = $"Erro ao realizar o pagamento.", StatusCode = (int)HttpStatusCode.NotFound });
        }
    }
}
