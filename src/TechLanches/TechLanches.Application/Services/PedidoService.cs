using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
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

        public async Task<List<Pedido>> BuscarPedidosPorStatus(StatusPedido statusPedido)
            => await _pedidoRepository.BuscarPedidosPorStatus(statusPedido);

        public async Task<Pedido> CadastrarPedido(int clienteId, List<ItemPedido> itemPedidos)
        {
            var pedido = new Pedido(clienteId, itemPedidos ?? new List<ItemPedido>());

            return await _pedidoRepository.CadastrarPedido(pedido);
        }
    }
}
