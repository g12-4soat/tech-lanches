using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<List<Pedido>> BuscarTodosPedidos()
            => await _pedidoRepository.BuscarTodosPedidos();

        public async Task<Pedido> BuscarPedidoPorId(int idPedido)
            => await _pedidoRepository.BuscarPedidoPorId(idPedido);
    }
}
