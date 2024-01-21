using Mapster;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TechLanches.Adapter.API.Constantes;
using Swashbuckle.AspNetCore.Annotations;
using TechLanches.Application.DTOs;
using TechLanches.Domain.ValueObjects;
using TechLanches.Application.Controllers.Interfaces;

namespace TechLanches.Adapter.API.Endpoints
{
    public static class ProdutoEndpoints
    {
        public static void MapProdutoEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapGet("api/produtos", BuscarTodosProdutos)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os produtos", description: "Retorna todos os produtos cadastrados"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(List<ProdutoResponseDTO>), description: "Produtos encontrados com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Produtos não encontrados"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapGet("api/produtos/{id}", BuscarProdutoPorId)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os produtos por id", description: "Retorna o produto cadastrado por id"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(ProdutoResponseDTO), description: "Produto encontrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Produto não encontrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapGet("api/produtos/categoria/{categoriaId}", BuscarProdutosPorCategoria)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Obter todos os produtos por id da categoria", description: "Retorna os produtos cadastrados por id da categoria"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(List<ProdutoResponseDTO>), description: "Produtos encontrados com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Produtos não encontrados"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapPost("api/produtos", CadastrarProduto)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Cadastrar produto", description: "Efetua o cadastramento do produto"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.Created, type: typeof(ProdutoResponseDTO), description: "Produto cadastrado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Produto não cadastrado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapPut("api/produtos", AtualizarProduto)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Atualizar produto", description: "Efetua a atualização do produto"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(ProdutoResponseDTO), description: "Produto atualizado com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Produto não atualizado"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapDelete("api/produtos/{id}", DeletarProduto)
               .WithTags(EndpointTagConstantes.TAG_PRODUTO)
               .WithMetadata(new SwaggerOperationAttribute(summary: "Remover produto", description: "Efetua a remoção do produto"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(ProdutoResponseDTO), description: "Produto removido com sucesso"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Produto não removido"))
               .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));

            app.MapGet("api/produtos/categorias", BuscarCategorias)
              .WithTags(EndpointTagConstantes.TAG_PRODUTO)
              .WithMetadata(new SwaggerOperationAttribute(summary: "Buscar categorias", description: "Buscar todas as categorias do produto"))
              .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.OK, type: typeof(List<CategoriaResponseDTO>), description: "Categorias encontradas"))
              .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.BadRequest, type: typeof(ErrorResponseDTO), description: "Requisição inválida"))
              .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.NotFound, type: typeof(ErrorResponseDTO), description: "Categorias não encontradas"))
              .WithMetadata(new SwaggerResponseAttribute((int)HttpStatusCode.InternalServerError, type: typeof(ErrorResponseDTO), description: "Erro no servidor interno"));
        }

        private static async Task<IResult> CadastrarProduto(
            [FromBody] ProdutoRequestDTO produtoRequest,
            [FromServices] IProdutoController produtoController)
        {
            var produto = await produtoController.Cadastrar(
                produtoRequest.Nome,
                produtoRequest.Descricao,
                produtoRequest.Preco,
                produtoRequest.CategoriaId);

            return produto is not null
                ? Results.Created($"api/produtos/{produto.Id}", produto)
                : Results.BadRequest(new ErrorResponseDTO { MensagemErro = "Erro ao cadastrar produto.", StatusCode = HttpStatusCode.BadRequest });
        }

        private static async Task<IResult> AtualizarProduto(
           int id,
           [FromBody] ProdutoRequestDTO produtoRequest,
           [FromServices] IProdutoController produtoController)
        {
            await produtoController.Atualizar(
                id,
                produtoRequest.Nome,
                produtoRequest.Descricao,
                produtoRequest.Preco,
                produtoRequest.CategoriaId);

            return produtoRequest is not null
                ? Results.Ok(produtoRequest.Adapt<ProdutoResponseDTO>())
                : Results.BadRequest(produtoRequest);
        }

        private static async Task<IResult> DeletarProduto(
          [FromRoute] int id,
          [FromServices] IProdutoController produtoController)
        {
            var produto = await produtoController.BuscarPorId(id);

            if (produto is null)
                return Results.NotFound(RetornarErroDtoNotFound());

            await produtoController.Deletar(id);
            return Results.Ok();
        }

        private static async Task<IResult> BuscarTodosProdutos(
           [FromServices] IProdutoController produtoController)
        {
            var produtos = await produtoController.BuscarTodos();

            if (produtos is null)
                return Results.NotFound(RetornarErroDtoNotFound());


            return Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>());
        }

        private static async Task<IResult> BuscarProdutoPorId(
           [FromRoute] int id,
           [FromServices] IProdutoController produtoController)
        {
            var produto = await produtoController.BuscarPorId(id);

            return produto is not null
                ? Results.Ok(produto)
                : Results.NotFound(RetornarErroDtoNotFound());
        }

        private static async Task<IResult> BuscarProdutosPorCategoria(
           [FromRoute] int categoriaId,
           [FromServices] IProdutoController produtoController)
        {
            var produtos = await produtoController.BuscarPorCategoria(categoriaId);

            return produtos is not null || produtos!.Count > 0
                ? Results.Ok(produtos.Adapt<List<ProdutoResponseDTO>>())
                : Results.NotFound(RetornarErroDtoNotFound());
        }

        private static async Task<IResult> BuscarCategorias()
        {
            var categorias = CategoriaProduto.List()
                .Select(x => new CategoriaResponseDTO { Id = x.Id, Nome = x.Nome })
                .ToList();

            return categorias is not null
                ? Results.Ok(await Task.FromResult(categorias))
                : Results.NotFound(new ErrorResponseDTO { MensagemErro = "Nenhuma categoria encontrada.", StatusCode = HttpStatusCode.BadRequest });
        }

        private static ErrorResponseDTO RetornarErroDtoNotFound()
        {
            return new ErrorResponseDTO { MensagemErro = $"Nenhum produto encontrado", StatusCode = HttpStatusCode.NotFound };
        }
    }
}
