using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.UseCases.Produtos
{
    public class ProdutoUseCases
    {
        public static async Task Atualizar(
            int produtoId,
            string nome,
            string descricao,
            decimal preco,
            int categoriaId,
            IProdutoGateway produtoGateway)
        {
            var produto = await VerificarProdutoExistente(produtoId, produtoGateway);

            produto.Atualizar(nome, descricao, preco, categoriaId);
        }

        public static async Task<Produto> Cadastrar(
            string nome,
            string descricao,
            decimal preco,
            int categoriaId,
            IProdutoGateway produtoGateway)
        {
            await VerificarProdutoNaoExistente(nome, produtoGateway);

            var produto = new Produto(nome, descricao, preco, categoriaId);

            var novoProduto = await produtoGateway.Cadastrar(produto);

            return novoProduto;
        }

        private static async Task<Produto> VerificarProdutoExistente(int id, IProdutoGateway produtoGateway)
        {
            var produto = await produtoGateway.BuscarPorId(id) 
                ?? throw new DomainException($"Produto não encontrado para o id: {id}");

            return produto;
        }

        private static async Task<Produto> VerificarProdutoNaoExistente(string nome, IProdutoGateway produtoGateway)
        {
            var produto = await produtoGateway.BuscarPorNome(nome);

            if (produto is not null)
                throw new DomainException($"Produto já cadastrado para o nome: {nome}");

            return produto;
        }
    }
}