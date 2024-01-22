using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Pedidos;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
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

        //TODO: Mudar quando trocar o dominio de cliente para clean arch 
        private readonly IClienteService _clienteService;

        public PedidoController(
            IPedidoGateway pedidoGateway,
            IPedidoPresenter pedidoPresenter,
            IClienteService clienteService,
            IStatusPedidoValidacaoService statusPedidoValidacaoService,
            IRabbitMqService rabbitmqService)
        {
            _pedidoGateway = pedidoGateway;
            _pedidoPresenter = pedidoPresenter;
            _clienteService = clienteService;
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
            var pedido = await PedidoUseCases.Cadastrar(cpf, itensPedido, _pedidoGateway, _clienteService);
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
