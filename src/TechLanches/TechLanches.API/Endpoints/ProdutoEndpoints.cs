using Microsoft.AspNetCore.Mvc;
using TechLanches.Domain.Services;

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
           string nome,
           string descricao,
           double preco,
           int categoriaId,
            [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.Cadastrar(nome, descricao, preco, categoriaId);

            return produto is not null
                ? Results.Ok(produto)
                : Results.BadRequest(new { nome, descricao, preco, categoriaId });
        }


        private static async Task<IResult> AtualizarProduto(
           int id,
           string nome,
           string descricao,
           double preco,
           int categoriaId,
            [FromServices] IProdutoService produtoService)
        {
            await produtoService.Atualizar(id, nome, descricao, preco, categoriaId);

            return Results.Ok(new { nome, descricao, preco, categoriaId });
        }

        private static async Task<IResult> DeletarProduto(
          [FromQuery] int id,
            [FromServices] IProdutoService produtoService)
        {
            await produtoService.Deletar(id);
            return Results.Ok();
        }

        private static async Task<IResult> BuscarTodosProdutos(
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarTodos();
            return Results.Ok(produtos);
        }

        private static async Task<IResult> BuscarProdutoPorId(
            [FromQuery] int id,
           [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);
            return produto is not null
                 ? Results.Ok(produto)
                 : Results.NotFound(new { id, error = "Produto não encontrado" });
        }

        private static async Task<IResult> BuscarProdutosPorCategoria(
           [FromQuery] int categoriaId,
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarPorCategoria(categoriaId);
            return Results.Ok(produtos);
        }
    }
}
