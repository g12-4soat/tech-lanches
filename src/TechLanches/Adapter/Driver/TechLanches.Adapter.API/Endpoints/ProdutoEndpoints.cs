using Mapster;
using Microsoft.AspNetCore.Mvc;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class ProdutoEndpoints
    {
        public static void MapProdutoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/produtos", BuscarTodosProdutos)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os produtos", description: "Retorna todos os produtos cadastrados"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(ProdutoResponseDTO), description: "Produtos encontrados com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Produtos não encontrados"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));
            
            app.MapGet("api/produtos/{id}", BuscarProdutoPorId)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os produtos por id", description: "Retorna o produto cadastrado por id"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(ProdutoResponseDTO), description: "Produto encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Produto não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

            app.MapGet("api/produtos/categoria/{categoriaId}", BuscarProdutosPorCategoria)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os produtos por id da categoria", description: "Retorna os produtos cadastrados por id da categoria"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(ProdutoResponseDTO), description: "Produtos encontrados com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Produtos não encontrados"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

            app.MapPost("api/produtos", CadastrarProduto)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Cadastrar produto", description: "Efetua o cadastramento do produto"))
               .WithMetadata(new SwaggerResponseAttribute(201, type: typeof(ProdutoResponseDTO), description: "Produto cadastrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Produto não cadastrado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

            app.MapPut("api/produtos", AtualizarProduto)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Atualizar produto", description: "Efetua a atualização do produto"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(ProdutoResponseDTO), description: "Produto atualizado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Produto não atualizado"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

            app.MapDelete("api/produtos/{id}", DeletarProduto)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Remover produto", description: "Efetua a remoção do produto"))
               .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(ProdutoResponseDTO), description: "Produto removido com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Produto não removido"))
               .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));

            app.MapGet("api/produtos/categorias", BuscarCategorias)
              .WithTags(EndpointTagConstantes.TAG_PRODUTO)
              .WithMetadata(new SwaggerOperationAttribute(summary: "Buscar categorias", description: "Buscar todas as categorias do produto"))
              .WithMetadata(new SwaggerResponseAttribute(200, type: typeof(CategoriaResponseDTO), description: "Categorias encontradas"))
              .WithMetadata(new SwaggerResponseAttribute(400, type: typeof(ProblemDetails), description: "Requisição inválida"))
              .WithMetadata(new SwaggerResponseAttribute(404, type: typeof(ProblemDetails), description: "Categorias não encontradas"))
              .WithMetadata(new SwaggerResponseAttribute(500, type: typeof(ProblemDetails), description: "Erro no servidor interno"));
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

            return produtoRequest is not null
                ? Results.Ok(produtoRequest.Adapt<ProdutoResponseDTO>())
                : Results.BadRequest(produtoRequest);
        }

        private static async Task<IResult> DeletarProduto(
          [FromRoute] int id,
          [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);
            if (produto is null)
                return Results.NotFound();

            await produtoService.Deletar(produto);
            return Results.Ok();
        }

        private static async Task<IResult> BuscarTodosProdutos(
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarTodos();

            return produtos is not null
                ? Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>())
                : Results.NotFound();
        }

        private static async Task<IResult> BuscarProdutoPorId(
           [FromRoute] int id,
           [FromServices] IProdutoService produtoService)
        {
            var produto = await produtoService.BuscarPorId(id);

            return produto is not null
                ? Results.Ok(produto.Adapt<ProdutoResponseDTO>())
                : Results.NotFound(id);
        }

        private static async Task<IResult> BuscarProdutosPorCategoria(
           [FromRoute] int categoriaId,
           [FromServices] IProdutoService produtoService)
        {
            var produtos = await produtoService.BuscarPorCategoria(categoriaId);

            return produtos is not null
                ? Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>())
                : Results.NotFound(categoriaId);
        }

        private static async Task<IResult> BuscarCategorias()
        {
            var categorias = CategoriaProduto.List()
                .Select(x => new CategoriaResponseDTO { Id = x.Id, Nome = x.Nome})
                .ToList();

            return categorias is not null
                ? Results.Ok(categorias)
                : Results.NotFound();
        }
    }
}
