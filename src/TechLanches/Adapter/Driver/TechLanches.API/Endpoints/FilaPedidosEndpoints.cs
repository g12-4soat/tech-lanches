using Microsoft.AspNetCore.Mvc;
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
            return Results.Ok(await pedidoService.BuscarPorStatus(StatusPedido.PedidoEmPreparacao));
        }
    }
}