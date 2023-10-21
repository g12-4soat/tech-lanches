using TechLanches.Application.Ports.Services;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Repositories;

namespace TechLanches.Application
{
    public class FilaPedidoService : IFilaPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public FilaPedidoService(IPedidoRepository pedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
        }

        public async Task<Pedido> RetornarPrimeiroPedidoDaFila()
        {
            var listaPedidos = await _pedidoRepository.BuscarTodos();

            var filaPedidos = listaPedidos.Where(x => 
                    x.StatusPedido == Domain.Enums.StatusPedido.PedidoCriado
                    || x.StatusPedido == Domain.Enums.StatusPedido.PedidoEmPreparacao)
                .OrderBy(x => x.Id)
                .ThenByDescending(x => x.StatusPedido)
                .ToList();

            return filaPedidos.FirstOrDefault();
        }

        public async Task TrocarStatus(Pedido pedido, StatusPedido statusPedido)
        {
            pedido.TrocarStatus(statusPedido);
            await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
