using TechLanches.Application.DTOs;

namespace TechLanches.Application.Controllers.Interfaces
{
    public interface IProdutoController
    {
        Task<List<ProdutoResponseDTO>> BuscarTodos();
        Task<ProdutoResponseDTO> BuscarPorId(int produtoId);
        Task<List<ProdutoResponseDTO>> BuscarPorCategoria(int categoriaId);
        Task<ProdutoResponseDTO> Cadastrar(string nome, string descricao, decimal preco, int categoriaId);
        Task Atualizar(int produtoId, string nome, string descricao, decimal preco, int categoriaId);
        Task Deletar(int produtoId);
    }
}
