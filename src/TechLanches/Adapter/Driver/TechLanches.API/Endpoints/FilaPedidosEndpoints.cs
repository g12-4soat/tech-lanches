using Mapster;
using Microsoft.AspNetCore.Mvc;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;

namespace TechLanches.API.Endpoints
{
    public static class FilaPedidosEndpoints
    {
        public static void MapFilaPedidoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/filapedidos", RetornarFilaPedidos).WithTags("FilaPedidos");
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