using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;

namespace TechLanches.Application.Controllers
{
    public class FilaPedidoController : IFilaPedidoController
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IPedidoPresenter _pedidoPresenter;
        private readonly IFilaPedidoGateway _filaPedidoGateway;
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;

        public FilaPedidoController(
            IPedidoRepository pedidoRepository, 
            IPedidoPresenter pedidoPresenter, 
            IFilaPedidoGateway filaPedidoGateway, 
            IStatusPedidoValidacaoService statusPedidoValidacaoService)
        {
            _pedidoGateway = new PedidoGateway(pedidoRepository);
            _pedidoPresenter = pedidoPresenter;
            _filaPedidoGateway = filaPedidoGateway;
            _statusPedidoValidacaoService = statusPedidoValidacaoService;
        }

        public async Task<PedidoResponseDTO?> RetornarPrimeiroPedidoDaFila()
        {
            Pedido? pedido = null;

            var filaPedidos = await _filaPedidoGateway.RetornarFilaPedidos();

            if (filaPedidos.Any())
            {
                var primeiroPedidoId = filaPedidos.First();
                pedido = await _pedidoGateway.BuscarPorId(primeiroPedidoId.PedidoId);
            }

            return pedido is not null ? _pedidoPresenter.ParaDto(pedido!) : null;
        }

        public async Task TrocarStatus(Pedido pedido, StatusPedido statusPedido)
        {
            pedido.TrocarStatus(_statusPedidoValidacaoService, statusPedido);
            await _pedidoGateway.CommitAsync();
        }
    }
}
