using Mapster;
using Microsoft.AspNetCore.Mvc;
using TechLanches.API.Constantes;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services;

namespace TechLanches.API.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/clientes/{cpf}", BuscarClientePorCpf).WithTags(EndpointTagConstantes.TAG_CLIENTE);
        app.MapPost("api/clientes", CadastrarCliente).WithTags(EndpointTagConstantes.TAG_CLIENTE);
    }

    private static async Task<IResult> BuscarClientePorCpf(
        [FromRoute] string cpf,
        [FromServices] IClienteService clienteService)
    {
        var cliente = await clienteService.BuscarPorCpf(cpf);

        return cliente is not null 
            ? Results.Ok(cliente.Adapt<ClienteResponseDTO>()) 
            : Results.NotFound(cpf);
    }

    private static async Task<IResult> CadastrarCliente(
        [FromBody] ClienteRequestDTO clienteRequest,
        [FromServices] IClienteService clienteService)
    {
        var cliente = await clienteService.Cadastrar(clienteRequest.Nome, clienteRequest.Email, clienteRequest.CPF);

        return cliente is not null 
            ? Results.Ok(cliente.Adapt<ClienteResponseDTO>()) 
            : Results.BadRequest(clienteRequest);
    }
}