﻿using Mapster;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.API.Endpoints;

public static class PedidoEndpoints
{
    public static void MapPedidoEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/pedidos", BuscarPedidos).WithTags("Pedidos");
        app.MapGet("api/pedidos/{idPedido}", BuscarPedidoPorId).WithTags("Pedidos");
        app.MapGet("api/pedidos/BuscarPedidosPorStatus/{statusPedido}", BuscarPedidosPorStatus).WithTags("Pedidos");
        app.MapPost("api/pedidos", CadastrarPedido).WithTags("Pedidos");
        app.MapPut("api/pedidos/{idPedido}", TrocarStatus).WithTags("Pedidos");
    }

    private static async Task<IResult> BuscarPedidos(
        [FromServices] IPedidoService pedidoService)
    {
        var pedidos = await pedidoService.BuscarTodos();

        return pedidos is not null
            ? Results.Ok(pedidos.Adapt<List<PedidoResponseDTO>>())
            : Results.BadRequest();
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
            : Results.BadRequest();
    }

    private static async Task<IResult> CadastrarPedido(
        [FromBody] PedidoRequestDTO pedidoDto,
        [FromServices] IPedidoService pedidoService, IClienteService clienteService, IProdutoService produtoService)
    {
        if (!pedidoDto.ItensPedido.Any())
            return Results.BadRequest("É necessário pelo menos 1 item para o pedido");

        int? clienteId = null;

        if(pedidoDto.Cpf is not null)
        {
            var clienteExistente = await clienteService.BuscarPorCpf(pedidoDto.Cpf);

            if(clienteExistente is null) return Results.BadRequest("Cliente não cadastrado!");

            clienteId = clienteExistente.Id;
        }

        var itensPedido = new List<ItemPedido>();

        foreach (var itemPedido in pedidoDto.ItensPedido)
        {
            var dadosProduto = await produtoService.BuscarPorId(itemPedido.IdProduto);
            var itemPedidoCompleto = new ItemPedido(dadosProduto.Id, itemPedido.Quantidade, dadosProduto.Preco);

            itensPedido.Add(itemPedidoCompleto);
        }

        var novoPedido = await pedidoService.Cadastrar(clienteId, itensPedido);

        return novoPedido is not null
            ? Results.Ok(novoPedido.Adapt<PedidoResponseDTO>())
            : Results.BadRequest();
    }

    private static async Task<IResult> TrocarStatus(
        int idPedido,
        [FromQuery] int statusPedido,
        [FromServices] IPedidoService pedidoService)
    {
        var pedido = await pedidoService.TrocarStatus(idPedido, (StatusPedido)statusPedido);

        return pedido is not null
            ? Results.Ok(pedido.Adapt<PedidoResponseDTO>())
            : Results.BadRequest();
    }
}