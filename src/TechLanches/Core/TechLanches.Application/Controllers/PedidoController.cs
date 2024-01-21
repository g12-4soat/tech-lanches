using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.Presenters.Interfaces;
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
            var cliente = await IdentificarCliente(cpf);
            var pedido = new Pedido(cliente?.Id, itensPedido);

            pedido = await _pedidoGateway.Cadastrar(pedido);
            await _pedidoGateway.CommitAsync();

            return _pedidoPresenter.ParaDto(pedido);
        }

        private async Task<Cliente?> IdentificarCliente(string? cpf)
        {
            if (cpf is null) return null;

            var clienteExistente = await _clienteService.BuscarPorCpf(cpf);

            if (clienteExistente is null) throw new DomainException("Cliente não cadastrado!");

            return clienteExistente;
        }

        public async Task<PedidoResponseDTO> TrocarStatus(int pedidoId, StatusPedido statusPedido)
        {
            var pedido = await _pedidoGateway.BuscarPorId(pedidoId)
               ?? throw new DomainException("Não foi encontrado nenhum pedido com id informado.");

            pedido.TrocarStatus(_statusPedidoValidacaoService, statusPedido);

            _pedidoGateway.Atualizar(pedido);

            if (statusPedido == StatusPedido.PedidoRecebido)
                _rabbitmqService.Publicar(pedidoId);

            await _pedidoGateway.CommitAsync();
            return _pedidoPresenter.ParaDto(pedido);
        }

        public async Task Atualizar(int pedidoId, int clienteId, List<ItemPedido> itensPedido)
        {
            var pedido = new Pedido(pedidoId, clienteId, itensPedido);

            _pedidoGateway.Atualizar(pedido);
            await _pedidoGateway.CommitAsync();
        }
    }
}
