using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;

namespace TechLanches.Application.Ports.Services
{
    public class FilaPedidoService : IFilaPedidoService
    {
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;
        private readonly IFilaPedidoRepository _filaPedidoRepository;
        private readonly IPedidoRepository _pedidoRepository;

        public FilaPedidoService(
            IPedidoRepository pedidoRepository,
            IFilaPedidoRepository filaPedidoRepository,
            IStatusPedidoValidacaoService statusPedidoValidacaoService)
        {
            _pedidoRepository = pedidoRepository;
            _filaPedidoRepository = filaPedidoRepository;
            _statusPedidoValidacaoService = statusPedidoValidacaoService;
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
            pedido.TrocarStatus(_statusPedidoValidacaoService, statusPedido);
            await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
