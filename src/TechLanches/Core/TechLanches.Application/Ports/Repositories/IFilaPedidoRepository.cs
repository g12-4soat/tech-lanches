using TechLanches.Application.DTOs;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IFilaPedidoRepository
    {
        Task<List<FilaPedido>> RetornarFilaPedidos();
    }
}