using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Produtos;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Controllers
{
    public class ProdutoController : IProdutoController
    {
        private readonly IProdutoGateway _produtoGateway;
        private readonly IProdutoPresenter _produtoPresenter;

        public ProdutoController(IProdutoRepository produtoRepository, IProdutoPresenter produtoPresenter)
        {
            _produtoGateway = new ProdutoGateway(produtoRepository);
            _produtoPresenter = produtoPresenter;
        }

        public async Task Atualizar(
            int produtoId,
            string nome,
            string descricao,
            decimal preco,
            int categoriaId)
        {
            await ProdutoUseCases.Atualizar(
                produtoId,
                nome,
                descricao,
                preco,
                categoriaId,
                _produtoGateway);

            await _produtoGateway.CommitAsync();
        }

        public async Task<List<ProdutoResponseDTO>> BuscarPorCategoria(int categoriaId)
        {
            var categoriaProduto = CategoriaProduto.From(categoriaId);
            var produtos = await _produtoGateway.BuscarPorCategoria(categoriaProduto);

            return _produtoPresenter.ParaListaDto(produtos);
        }

        public async Task<ProdutoResponseDTO> BuscarPorId(int produtoId)
        {
            var produto = await _produtoGateway.BuscarPorId(produtoId);

            return _produtoPresenter.ParaDto(produto);
        }

        public async Task<List<ProdutoResponseDTO>> BuscarTodos()
        {
            var produtos = await _produtoGateway.BuscarTodos();

            return _produtoPresenter.ParaListaDto(produtos);
        }

        public async Task<ProdutoResponseDTO> Cadastrar(string nome, string descricao, decimal preco, int categoriaId)
        {
            var novoProduto = await ProdutoUseCases.Cadastrar(nome, descricao, preco, categoriaId, _produtoGateway);

            await _produtoGateway.CommitAsync();

            return _produtoPresenter.ParaDto(novoProduto);
        }

        public async Task Deletar(int produtoId)
        {
            var produto = await _produtoGateway.BuscarPorId(produtoId);

            produto.DeletarProduto();

            await _produtoGateway.CommitAsync();
        }
    }
}
