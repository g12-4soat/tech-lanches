using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Gateways
{
    public class PedidoGateway : IPedidoGateway
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoGateway(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public Task<List<Pedido>> BuscarTodos()
        {
            return _pedidoRepository.BuscarTodos();
        }

        public Task<Pedido> BuscarPorId(int idPedido)
        {
            return _pedidoRepository.BuscarPorId(idPedido);
        }

        public Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
        {
            return _pedidoRepository.BuscarPorStatus(statusPedido);
        }

        public Task<Pedido> Cadastrar(Pedido pedido)
        {
            return _pedidoRepository.Cadastrar(pedido);
        }

        public async Task CommitAsync()
        {
            await _pedidoRepository.UnitOfWork.CommitAsync();
        }
    }
}
