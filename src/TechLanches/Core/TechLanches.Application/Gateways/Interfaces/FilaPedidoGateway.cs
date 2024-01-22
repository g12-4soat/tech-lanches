using TechLanches.Application.DTOs;
using TechLanches.Application.Ports.Repositories;

namespace TechLanches.Application.Gateways.Interfaces
{
    public class FilaPedidoGateway : IFilaPedidoGateway
    {
        private readonly IFilaPedidoRepository _filaPedidoRepository;

        public FilaPedidoGateway(IFilaPedidoRepository filaPedidoRepository)
        {
            _filaPedidoRepository = filaPedidoRepository;
        }

        public Task<List<FilaPedido>> RetornarFilaPedidos()
        {
            return _filaPedidoRepository.RetornarFilaPedidos();
        }
    }
}
