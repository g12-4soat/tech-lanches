using Mapster;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class FilaPedidosEndpoints
    {
        public static void MapFilaPedidoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/filapedidos", RetornarFilaPedidos)
               .WithTags(EndpointTagConstantes.TAG_FILA_PEDIDO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os pedidos da fila", description: "Retorna todos os pedidos contidos na fila"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedidos da fila encontrados com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Pedidos da fila não encontrados"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> RetornarFilaPedidos(
            [FromServices] IPedidoService pedidoService)
        {
            var pedidos = await pedidoService.BuscarPorStatus(StatusPedido.PedidoEmPreparacao);
            return pedidos is not null
                ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
                : Results.BadRequest();
        }
    }
}