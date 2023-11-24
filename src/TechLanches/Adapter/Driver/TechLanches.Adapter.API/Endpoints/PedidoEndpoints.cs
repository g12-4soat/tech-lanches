using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
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
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pedidos não encontrados"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

        app.MapGet("api/pedidos/{idPedido}", BuscarPedidoPorId)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os pedidos por id", description: "Retorna o pedido cadastrado por id"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedido encontrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pedido não encontrado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

        app.MapGet("api/pedidos/buscarpedidosporstatus/{statusPedido}", BuscarPedidosPorStatus)
          .WithTags(EndpointTagConstantes.TAG_PEDIDO)
          .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os pedidos por status", description: "Retorna todos os pedidos contidos no status"))
          .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Pedidos encontrados com sucesso"))
          .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
          .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pedidos não encontrados"))
          .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

        app.MapPost("api/pedidos", CadastrarPedido)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Cadastrar pedido", description: "Efetua o cadastramento do pedido"))
           .WithMetadata(new SwaggerResponseAttribute(201, type: typeof(PedidoResponseDTO), description: "Pedido cadastrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Pedido não cadastrado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

        app.MapPut("api/pedidos/{idPedido}/trocarstatus", TrocarStatus)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Trocar status do pedido", description: "Efetua a troca de status do pedido"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(PedidoResponseDTO), description: "Status do pedido alterado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Status do pedido não alterado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

        app.MapGet("api/pedidos/statuspedidos", BuscarStatusPedidos)
           .WithTags(EndpointTagConstantes.TAG_PEDIDO)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Buscar status pedidos", description: "Retorna todos os status de pedidos"))
           .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(StatusPedidoResponseDTO), description: "Status do pedido encontrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ErrorResponseDTO), description: "Status do pedido não encontrado"))
           .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
    }

    private static async Task<IResult> BuscarPedidos(
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarTodos();

        return pedidos is not null
            ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
            : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao buscar pedidos.", StatusCode = (int)HttpStatusCode.BadRequest});
    }

    private static async Task<IResult> BuscarPedidoPorId(
        int idPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedido = await pedidoService.BuscarPorId(idPedido);

        return pedido is not null
            ? Results.Ok(pedido.Adapt<PedidoResponseDTO>())
            : Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Pedido não encontrado para o id: {idPedido}.", StatusCode = (int)HttpStatusCode.NotFound });
    }

    private static async Task<IResult> BuscarPedidosPorStatus(
        StatusPedido statusPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarPorStatus(statusPedido);

        return pedidos is not null
            ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
            : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao buscar pedidos por status.", StatusCode = (int)HttpStatusCode.BadRequest });
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
            : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao cadastrar pedido.", StatusCode = (int)HttpStatusCode.BadRequest });
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
            : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao trocar status.", StatusCode = (int)HttpStatusCode.BadRequest });
    }

    private static async Task<IResult> BuscarStatusPedidos()
    {
        var statusPedidos = Enum.GetValues(typeof(StatusPedido))
            .Cast<StatusPedido>()
            .Select(x => new StatusPedidoResponseDTO { Id = (int)x, Nome = x.ToString() })
            .ToList();

        return statusPedidos is not null
            ? Results.Ok(statusPedidos)
            :Results.NotFound(new ErrorResponseDTO { MensagemErro = "Nenhum status encontrado.", StatusCode = (int)HttpStatusCode.NotFound });
    }
}