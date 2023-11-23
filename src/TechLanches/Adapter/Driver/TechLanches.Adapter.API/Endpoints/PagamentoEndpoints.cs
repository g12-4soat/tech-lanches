using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
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
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PagamentoResponseDTO), description: "Status do pagamento encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pedido não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno")); ;

            app.MapPost("api/pagamentos", BuscarPagamento).WithTags(EndpointTagConstantes.TAG_PAGAMENTO);
        }

        private static async Task<IResult> BuscarStatusPagamentoPorPedidoId([FromRoute] int pedidoId, [FromServices] IPagamentoService pagamentoService)
        {
            var pagamento = await pagamentoService.BuscarStatusPagamentoPorPedidoId(pedidoId);

            if (pagamento is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum pedido encontrado para o id: {pedidoId}", StatusCode = (int)HttpStatusCode.NotFound });


            return Results.Ok(pagamento.Adapt<PagamentoResponseDTO>());
        }

        private static async Task<IResult> BuscarPagamento()///?????
        {
            throw new NotImplementedException();
        }
    }
}
