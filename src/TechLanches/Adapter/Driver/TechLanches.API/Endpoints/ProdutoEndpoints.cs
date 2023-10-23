using Mapster;
using Microsoft.AspNetCore.Mvc;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services;

namespace TechLanches.API.Endpoints
{
    public static class ProdutoEndpoints
    {
        public static void MapProdutoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/produtos", BuscarTodosProdutos);
            app.MapGet("api/produtos/{id}", BuscarProdutoPorId);
            app.MapGet("api/produtos/categoria/{categoriaId}", BuscarProdutosPorCategoria);
            app.MapPost("api/produtos", CadastrarProduto);
            app.MapPut("api/produtos", AtualizarProduto);
            app.MapDelete("api/produtos/{id}", DeletarProduto);
        }

        private static async Task<IResult> CadastrarProduto(
            [FromBody] ProdutoRequestDTO produtoRequest,
            [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.Cadastrar(produtoRequest.Nome, produtoRequest.Descricao, produtoRequest.Preco, produtoRequest.CategoriaId);

            return produto is not null
                ? Results.Ok(produto.Adapt<ProdutoResponseDTO>())
                : Results.BadRequest(produtoRequest);
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
          [FromQuery] int id,
          [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);
            if (produto is null)
                return Results.NotFound(new { error = $"Nenhum produto encontrado para o id: {id}." });

            await produtoService.Deletar(produto);
            return Results.Ok();
        }

        private static async Task<IResult> BuscarTodosProdutos(
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarTodos();

            if (produtos is null)
                return Results.NotFound(new { error = "Nenhum produto encontrado." });

            return Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>());
        }

        private static async Task<IResult> BuscarProdutoPorId(
           [FromQuery] int id,
           [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);
            if(produto is null)
                return Results.NotFound(new { error = $"Nenhum produto encontrado para o id: {id}." });

            return Results.Ok(produto.Adapt<ProdutoResponseDTO>());
        }

        private static async Task<IResult> BuscarProdutosPorCategoria(
           [FromQuery] int categoriaId,
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarPorCategoria(categoriaId);

            if (produtos is null)
                return Results.NotFound(new { error = "Nenhum produto encontrado" });

            return Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>());
        }
    }
}
