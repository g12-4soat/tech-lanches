using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class ProdutoEndpoints
    {
        public static void MapProdutoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/produtos", BuscarTodosProdutos).WithTags(EndpointTagConstantes.TAG_PRODUTO);
            app.MapGet("api/produtos/{id}", BuscarProdutoPorId).WithTags(EndpointTagConstantes.TAG_PRODUTO);
            app.MapGet("api/produtos/categoria/{categoriaId}", BuscarProdutosPorCategoria).WithTags(EndpointTagConstantes.TAG_PRODUTO);
            app.MapPost("api/produtos", CadastrarProduto).WithTags(EndpointTagConstantes.TAG_PRODUTO);
            app.MapPut("api/produtos", AtualizarProduto).WithTags(EndpointTagConstantes.TAG_PRODUTO);
            app.MapDelete("api/produtos/{id}", DeletarProduto).WithTags(EndpointTagConstantes.TAG_PRODUTO);
        }

        private static async Task<IResult> CadastrarProduto(
            [FromBody] ProdutoRequestDTO produtoRequest,
            [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.Cadastrar(produtoRequest.Nome, produtoRequest.Descricao, produtoRequest.Preco, produtoRequest.CategoriaId);

            return produto is not null
                ? Results.Ok(produto.Adapt<ProdutoResponseDTO>())
            : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao cadastrar produto.", StatusCode = (int)HttpStatusCode.BadRequest});

        }


        private static async Task<IResult> AtualizarProduto(
           int id,
           [FromBody] ProdutoRequestDTO produtoRequest,
           [FromServices] IProdutoService produtoService)
        {
            await produtoService.Atualizar(id, produtoRequest.Nome, produtoRequest.Descricao, produtoRequest.Preco, produtoRequest.CategoriaId);

            return Results.Ok(produtoRequest);
        }

        private static async Task<IResult> DeletarProduto(
          [FromRoute] int id,
          [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);
            if (produto is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum produto encontrado para o id: {id}", StatusCode = (int)HttpStatusCode.NotFound });

            await produtoService.Deletar(produto);
            return Results.Ok();
        }

        private static async Task<IResult> BuscarTodosProdutos(
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarTodos();

            if (produtos is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum produto encontrado", StatusCode = (int)HttpStatusCode.NotFound });


            return Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>());
        }

        private static async Task<IResult> BuscarProdutoPorId(
           [FromRoute] int id,
           [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);
            if (produto is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum produto encontrado para o id: {id}", StatusCode = (int)HttpStatusCode.NotFound });

            return Results.Ok(produto.Adapt<ProdutoResponseDTO>());
        }

        private static async Task<IResult> BuscarProdutosPorCategoria(
           [FromRoute] int categoriaId,
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarPorCategoria(categoriaId);

            if (produtos is null)
                return Results.NotFound(new ErrorResponseDTO { MensagemErro = $"Nenhum produto encontrado para a categoria id: {categoriaId}", StatusCode = (int)HttpStatusCode.NotFound });


            return Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>());
        }
    }
}
