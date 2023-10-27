using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
            app.MapGet("api/filapedidos", RetornarFilaPedidos).WithTags(EndpointTagConstantes.TAG_FILA_PEDIDO);
        }

        private static async Task<IResult> RetornarFilaPedidos(
            [FromServices] IPedidoService pedidoService)
        {
            var pedidos = await pedidoService.BuscarPorStatus(StatusPedido.PedidoEmPreparacao);
            return pedidos is not null
                ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
                : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao retornar fila pedido.", StatusCode = (int)HttpStatusCode.BadRequest });
            ;
        }
    }
}