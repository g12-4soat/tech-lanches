using Microsoft.AspNetCore.Mvc;
using TechLanches.Domain.Services;

namespace TechLanches.API.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/clientes/{cpf}", BuscarClientePorCpf);
        app.MapPost("api/clientes", CadastrarCliente);
    }

    private static async Task<IResult> BuscarClientePorCpf(
        [FromQuery] string cpf,
        [FromServices] IClienteService clienteService)
    {
        var cliente = await clienteService.BuscarPorCpf(cpf);

        return cliente is not null 
            ? Results.Ok(cliente) 
            : Results.NotFound(cpf);
    }

    private static async Task<IResult> CadastrarCliente(
        string nome,
        string email,
        string cpf,
        [FromServices] IClienteService clienteService)
    {
        var cliente = await clienteService.Cadastrar(nome, email, cpf);

        return cliente is not null 
            ? Results.Ok(cliente) 
            : Results.BadRequest(new { nome, email, cpf });
    }
}