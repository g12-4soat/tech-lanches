using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;


namespace TechLanches.API.Endpoints;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/pedidos", BuscarPedidos).WithTags("Pedidos");
        app.MapGet("api/pedidos/{idPedido}", BuscarPedidoPorId).WithTags("Pedidos");
        app.MapGet("api/pedidos/BuscarPedidosPorStatus/{statusPedidoId}", BuscarPedidosPorStatus).WithTags("Pedidos");
        app.MapPost("api/pedidos", CadastrarPedido).WithTags("Pedidos");
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
        [FromQuery] StatusPedido statusPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarPedidosPorStatus(statusPedido);

        return pedidos is not null
            ? Results.Ok(pedidos)
            : Results.BadRequest();
    }

    private static async Task<IResult> CadastrarPedido(
        [FromBody] PedidoDTO pedidoDto,
        [FromServices] IPedidoService pedidoService)
    {
        var pedido = await pedidoService.CadastrarPedido(pedidoDto.ClienteId, pedidoDto.ItensPedido);

        string json = JsonConvert.SerializeObject(pedido.Id, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
        });

        return pedido is not null
            ? Results.Ok(json)
            : Results.BadRequest();
    }
}