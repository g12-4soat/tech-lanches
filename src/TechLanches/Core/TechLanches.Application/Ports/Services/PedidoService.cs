using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IStatusPedidoValidacaoService _statusPedidoValidacaoService;
        private readonly IClienteService _clienteService;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IRabbitMqService _rabbitmqService;

        public PedidoService(
            IPedidoRepository pedidoRepository,
            IClienteService clienteService,
            IStatusPedidoValidacaoService statusPedidoValidacaoService,            
            IRabbitMqService rabbitmqService)
        {
            _pedidoRepository = pedidoRepository;
            _clienteService = clienteService;
            _statusPedidoValidacaoService = statusPedidoValidacaoService;           
            _rabbitmqService = rabbitmqService;
        }

        public async Task<List<Pedido>> BuscarTodos()
            => await _pedidoRepository.BuscarTodos();

        public async Task<Pedido> BuscarPorId(int idPedido)
            => await _pedidoRepository.BuscarPorId(idPedido);

        public async Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
            => await _pedidoRepository.BuscarPorStatus(statusPedido);

        public async Task<Pedido> Cadastrar(string? cpf, List<ItemPedido> itensPedido)
        {
            var cliente = await IdentificarCliente(cpf);
            var pedido = new Pedido(cliente?.Id, itensPedido);

            pedido = await _pedidoRepository.Cadastrar(pedido);
            await _pedidoRepository.UnitOfWork.CommitAsync();

            return pedido;
        }

        private async Task<Cliente?> IdentificarCliente(string? cpf)
        {
            if (cpf is null) return null;

            var clienteExistente = await _clienteService.BuscarPorCpf(cpf);

            if (clienteExistente is null) throw new DomainException("Cliente não cadastrado!");

            return clienteExistente;
        }

        public async Task<Pedido> TrocarStatus(int pedidoId, StatusPedido statusPedido)
        {
            var pedido = await _pedidoRepository.BuscarPorId(pedidoId)
                ?? throw new DomainException("Não foi encontrado nenhum pedido com id informado.");

            pedido.TrocarStatus(_statusPedidoValidacaoService, statusPedido);

            _pedidoRepository.Atualizar(pedido);            

            if (statusPedido == StatusPedido.PedidoRecebido)
                _rabbitmqService.Publicar(pedidoId);

            await _pedidoRepository.UnitOfWork.CommitAsync();
            return pedido;
        }

        public async Task Atualizar(int pedidoId, int clienteId, List<ItemPedido> itensPedido)
        {
            var pedido = new Pedido(pedidoId, clienteId, itensPedido);

            _pedidoRepository.Atualizar(pedido);
            await _pedidoRepository.UnitOfWork.CommitAsync();
        }
    }
}
