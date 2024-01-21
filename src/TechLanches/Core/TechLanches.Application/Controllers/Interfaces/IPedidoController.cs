using TechLanches.Application.DTOs;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Controllers.Interfaces
{
    public interface IPedidoController
    {
        Task<List<PedidoResponseDTO>> BuscarTodos();
        Task<PedidoResponseDTO> BuscarPorId(int idPedido);
        Task<List<PedidoResponseDTO>> BuscarPorStatus(StatusPedido statusPedido);
        Task<PedidoResponseDTO> Cadastrar(string? cpf, List<ItemPedido> itensPedido);
        Task<PedidoResponseDTO> TrocarStatus(int pedidoId, StatusPedido statusPedido);
        Task Atualizar(int pedidoId, int clienteId, List<ItemPedido> itensPedido);
    }
}
