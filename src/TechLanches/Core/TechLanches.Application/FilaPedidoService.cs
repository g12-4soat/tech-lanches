using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Repositories;

namespace TechLanches.Application
{
    public class FilaPedidoService : IFilaPedidoService
    {
        private readonly IFilaPedidoRepository _filaPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public FilaPedidoService(
            IPedidoRepository pedidoRepository,
            IFilaPedidoRepository filaPedidoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _filaPedidoRepository = filaPedidoRepository;
        }

        public async Task<Pedido?> RetornarPrimeiroPedidoDaFila()
        {
            Pedido? pedido = null;

            var filaPedidos = await _filaPedidoRepository.RetornarFilaPedidos();
            
            if (filaPedidos.Any())
            {
                var primeiroPedidoId = filaPedidos.First();
                pedido = await _pedidoRepository.BuscarPorId(primeiroPedidoId.PedidoId);
            }

            return pedido;
        }

        public async Task TrocarStatus(Pedido pedido, StatusPedido statusPedido)
        {
            pedido.TrocarStatus(statusPedido);
            await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
