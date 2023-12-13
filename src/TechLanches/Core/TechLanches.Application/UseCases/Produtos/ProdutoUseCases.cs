using TechLanches.Application.Ports.Repositories;
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
            IProdutoRepository produtoRepository)
        {
            var produto = await VerificarProdutoExistente(produtoId, produtoRepository);

            produto.Atualizar(nome, descricao, preco, categoriaId);
        }

        public static async Task<Produto> Cadastrar(
            string nome,
            string descricao,
            decimal preco,
            int categoriaId,
            IProdutoRepository produtoRepository)
        {
            await VerificarProdutoNaoExistente(nome, produtoRepository);

            var produto = new Produto(nome, descricao, preco, categoriaId);

            var novoProduto = await produtoRepository.Cadastrar(produto);

            return novoProduto;
        }

        private static async Task<Produto> VerificarProdutoExistente(int id, IProdutoRepository produtoRepository)
        {
            var produto = await produtoRepository.BuscarPorId(id);

            if (produto is null)
                throw new DomainException($"Produto não encontrado para o id: {id}");

            return produto;
        }

        private static async Task<Produto> VerificarProdutoNaoExistente(string nome, IProdutoRepository produtoRepository)
        {
            var produto = await produtoRepository.BuscarPorNome(nome);

            if (produto is not null)
                throw new DomainException($"Produto já cadastrado para o nome: {nome}");

            return produto;
        }
    }
}
