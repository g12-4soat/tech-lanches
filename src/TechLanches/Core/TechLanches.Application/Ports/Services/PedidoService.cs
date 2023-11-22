using Mapster;
using TechLanches.Adapter.ACL.Pagamento.QrCode;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPagamentoService _pagamentoService;
        private readonly IClienteService _clienteService;
        private readonly IPagamentoQrCodeACLService _pagamentoACLService;

        public PedidoService(IPedidoRepository pedidoRepository, IPagamentoService pagamentoService, IClienteService clienteService,
            IPagamentoQrCodeACLService pagamentoACLService)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoService = pagamentoService;
            _clienteService = clienteService;
            _pagamentoACLService = pagamentoACLService;
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
            await _pedidoRepository.UnitOfWork.Commit();

            var qrCode = await _pagamentoACLService.GerarQrCode(pedido.Adapt<PedidoACLDTO>());
            var pagamento = await _pagamentoService.RealizarPagamento(pedido.Id, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            if (pagamento) return pedido;

            else throw new DomainException("Pagamento não autorizado!");
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
            pedido.TrocarStatus(statusPedido);
            _pedidoRepository.Atualizar(pedido);
            await _pedidoRepository.UnitOfWork.Commit();
            return pedido;
        }

        public async Task Atualizar(int pedidoId, int clienteId, List<ItemPedido> itensPedido)
        {
            var pedido = new Pedido(pedidoId, clienteId, itensPedido);

            _pedidoRepository.Atualizar(pedido);
            await _pedidoRepository.UnitOfWork.Commit();
        }
    }
}
