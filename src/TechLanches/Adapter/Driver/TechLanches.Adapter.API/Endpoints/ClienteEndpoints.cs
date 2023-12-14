using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;

namespace TechLanches.Adapter.API.Endpoints;

public static class ClienteEndpoints
{
    public static void MapClienteEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/clientes/{cpf}", BuscarClientePorCpf)
           .WithTags(EndpointTagConstantes.TAG_CLIENTE)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Obter cliente por CPF", description: "Retorna o cliente proprietário do CPF"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(ClienteResponseDTO), description: "Cliente encontrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Cliente não encontrado"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

        app.MapPost("api/clientes", CadastrarCliente)
           .WithTags(EndpointTagConstantes.TAG_CLIENTE)
           .WithMetadata(new SwaggerOperationAttribute(summary: "Cadastrar cliente", "Efetua o cadastramento do cliente"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(ClienteResponseDTO), description: "Cliente cadastrado com sucesso"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Cliente não cadastrado"))
           .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
    }

    private static async Task<IResult> BuscarClientePorCpf(
        [FromRoute] string cpf,
        [FromServices] IClienteService clienteService)
    {
        var cliente = await clienteService.BuscarPorCpf(cpf);

        return cliente is not null
            ? Results.Ok(cliente.Adapt<ClienteResponseDTO>())
            : Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Cliente não encontrado para o CPF: {cpf}.", StatusCode = HttpStatusCode.NotFound} );
    }

    private static async Task<IResult> CadastrarCliente(
        [FromBody] ClienteRequestDTO clienteRequest,
        [FromServices] IClienteService clienteService)
    {
        var cliente = await clienteService.Cadastrar(clienteRequest.Nome, clienteRequest.Email, clienteRequest.CPF);

        return cliente is not null
            ? Results.Ok(cliente.Adapt<ClienteResponseDTO>())
            : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Cliente inválido.", StatusCode = HttpStatusCode.BadRequest });
    }
}