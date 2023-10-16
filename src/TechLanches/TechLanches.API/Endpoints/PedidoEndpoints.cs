using Microsoft.AspNetCore.Mvc;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.API.Endpoints;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/pedidos", BuscarPedidos).WithTags("Pedidos");
        app.MapGet("api/pedidos/{idPedido}", BuscarPedidoPorId).WithTags("Pedidos");
        app.MapGet("api/pedidos/BuscarPedidosPorStatus/{statusPedidoId}", BuscarPedidosPorStatus).WithTags("Pedidos");
    }

    private static async Task<IResult> BuscarPedidos(
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarTodosPedidos();

        return pedidos is not null
            ? Results.Ok(pedidos)
            : Results.BadRequest();
    }

    private static async Task<IResult> BuscarPedidoPorId(
        [FromQuery] int idPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedido = await pedidoService.BuscarPedidoPorId(idPedido);

        return pedido is not null
            ? Results.Ok(pedido)
            : Results.NotFound(idPedido);
    }

    private static async Task<IResult> BuscarPedidosPorStatus(
        [FromQuery] int statusPedidoId,
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarPedidosPorStatus(StatusPedido.From(statusPedidoId));

        return pedidos is not null
            ? Results.Ok(pedidos)
            : Results.BadRequest();
    }
}