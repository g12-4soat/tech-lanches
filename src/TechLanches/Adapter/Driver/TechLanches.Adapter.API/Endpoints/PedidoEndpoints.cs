using Mapster;
using Microsoft.AspNetCore.Mvc;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.API.Endpoints;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/pedidos", BuscarPedidos)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os pedidos", description: "Retorna todos os pedidos cadastrados"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedidos encontrados com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Pedidos não encontrados"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

        app.MapGet("api/pedidos/{idPedido}", BuscarPedidoPorId)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os pedidos por id", description: "Retorna o pedido cadastrado por id"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedido encontrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Pedido não encontrado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

        app.MapGet("api/pedidos/BuscarPedidosPorStatus/{statusPedido}", BuscarPedidosPorStatus)
          .WithTags(EndpointTagConstantes.TAG_PEDIDO)
          .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os pedidos por status", description: "Retorna todos os pedidos contidos no status"))
          .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedidos encontrados com sucesso"))
          .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
          .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Pedidos não encontrados"))
          .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

        app.MapPost("api/pedidos", CadastrarPedido)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Cadastrar pedido", description: "Efetua o cadastramento do pedido"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedido cadastrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Pedido não cadastrado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

        app.MapPut("api/pedidos/{idPedido}", TrocarStatus)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Trocar status do pedido", description: "Efetua a troca de status do pedido"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Status do pedido alterado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Status do pedido não alterado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));
    }

    private static async Task<IResult> BuscarPedidos(
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarTodos();

        return pedidos is not null
            ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
            : Results.NotFound();
    }

    private static async Task<IResult> BuscarPedidoPorId(
        int idPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedido = await pedidoService.BuscarPorId(idPedido);

        return pedido is not null
            ? Results.Ok(pedido.Adapt<PedidoResponseDTO>())
            : Results.NotFound(idPedido);
    }

    private static async Task<IResult> BuscarPedidosPorStatus(
        StatusPedido statusPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarPorStatus(statusPedido);

        return pedidos is not null
            ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
            : Results.NotFound(statusPedido);
    }

    private static async Task<IResult> CadastrarPedido(
        [FromBody] PedidoRequestDTO pedidoDto,
        [FromServices] IPedidoService pedidoService, IClienteService clienteService, IProdutoService produtoService)
    {
        if (!pedidoDto.ItensPedido.Any())
            return Results.BadRequest(MensagensConstantes.SEM_NENHUM_ITEM_PEDIDO);

        var itensPedido = new List<ItemPedido>();
        foreach (var itemPedido in pedidoDto.ItensPedido)
        {
            var dadosProduto = await produtoService.BuscarPorId(itemPedido.IdProduto);
            var itemPedidoCompleto = new ItemPedido(dadosProduto.Id, itemPedido.Quantidade, dadosProduto.Preco);

            itensPedido.Add(itemPedidoCompleto);
        }

        var novoPedido = await pedidoService.Cadastrar(pedidoDto.Cpf, itensPedido);

        return novoPedido is not null
            ? Results.Ok(novoPedido.Adapt<PedidoResponseDTO>())
            : Results.BadRequest();
    }

    private static async Task<IResult> TrocarStatus(
        [FromRoute] int idPedido,
        [FromBody] int statusPedido,
        [FromServices] IPedidoService pedidoService)
    {
        if (!Enum.IsDefined(typeof(StatusPedido), statusPedido))
            return Results.BadRequest();

        var pedido = await pedidoService.TrocarStatus(idPedido, (StatusPedido)statusPedido);

        return pedido is not null
            ? Results.Ok(pedido.Adapt<PedidoResponseDTO>())
            : Results.BadRequest();
    }
}