using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Pedidos;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Controllers
{
    public class PedidoController : IPedidoController
    {
        private readonly IPedidoGateway _pedidoGateway;
        private readonly IPedidoPresenter _pedidoPresenter;
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;
        private readonly IRabbitMqService _rabbitmqService;
        private readonly IClienteGateway _clienteGateway;

        public PedidoController(
            IPedidoRepository pedidoRepository,
            IPedidoPresenter pedidoPresenter,
            IClienteRepository clienteRepository,
            IStatusPedidoValidacaoService statusPedidoValidacaoService,
            IRabbitMqService rabbitmqService)
        {
            _pedidoGateway = new PedidoGateway(pedidoRepository);
            _pedidoPresenter = pedidoPresenter;
            _clienteGateway = new ClienteGateway(clienteRepository);
            _statusPedidoValidacaoService = statusPedidoValidacaoService;
            _rabbitmqService = rabbitmqService;
        }

        public async Task<List<PedidoResponseDTO>> BuscarTodos()
        {
            var pedidos = await _pedidoGateway.BuscarTodos();

            return _pedidoPresenter.ParaListaDto(pedidos);
        }

        public async Task<PedidoResponseDTO> BuscarPorId(int idPedido)
        {
            var pedido = await _pedidoGateway.BuscarPorId(idPedido);

            return _pedidoPresenter.ParaDto(pedido);
        }

        public async Task<List<PedidoResponseDTO>> BuscarPorStatus(StatusPedido statusPedido)
        {
            var pedidos = await _pedidoGateway.BuscarPorStatus(statusPedido);

            return _pedidoPresenter.ParaListaDto(pedidos);
        }

        public async Task<PedidoResponseDTO> Cadastrar(string? cpf, List<ItemPedido> itensPedido)
        {
            var pedido = await PedidoUseCases.Cadastrar(cpf, itensPedido, _pedidoGateway, _clienteGateway);
            await _pedidoGateway.CommitAsync();

            return _pedidoPresenter.ParaDto(pedido);
        }

        public async Task<PedidoResponseDTO> TrocarStatus(int pedidoId, StatusPedido statusPedido)
        {
            var pedido = await PedidoUseCases.TrocarStatus(pedidoId, statusPedido, _pedidoGateway, _statusPedidoValidacaoService, _rabbitmqService);
            await _pedidoGateway.CommitAsync();

            return _pedidoPresenter.ParaDto(pedido);
        }
    }
}
