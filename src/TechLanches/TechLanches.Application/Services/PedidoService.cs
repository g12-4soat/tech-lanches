using TechLanches.Core;
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

        public async Task<List<Pedido>> BuscarTodos()
            => await _pedidoRepository.BuscarTodos();

        public async Task<Pedido> BuscarPorId(int idPedido)
            => await _pedidoRepository.BuscarPorId(idPedido);

        public async Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
            => await _pedidoRepository.BuscarPorStatus(statusPedido);

        public async Task<Pedido> Cadastrar(int clienteId, List<ItemPedido> itemPedidos)
        {
            var pedido = new Pedido(clienteId, itemPedidos ?? new List<ItemPedido>());

            return await _pedidoRepository.Cadastrar(pedido);
        }

        public async Task<Pedido> TrocarStatus(int pedidoId, StatusPedido statusPedido)
        {
            var pedido = await _pedidoRepository.BuscarPorId(pedidoId) 
                ?? throw new DomainException("Não foi encontrado nenhum pedido com id informado.");
            pedido.TrocarStatus(statusPedido);
            _pedidoRepository.Atualizar(pedido);
            await _pedidoRepository.UnitOfWork.Commit();
            return pedido;
        }
    }
}
