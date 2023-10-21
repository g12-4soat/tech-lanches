using TechLanches.Application.DTOs;
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
        private readonly IPagamentoService _pagamentoService;

        

        public PedidoService(IPedidoRepository pedidoRepository, IPagamentoService pagamentoService)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoService = pagamentoService;
        }

        public async Task<List<Pedido>> BuscarTodos()
            => await _pedidoRepository.BuscarTodos();

        public async Task<Pedido> BuscarPorId(int idPedido)
            => await _pedidoRepository.BuscarPorId(idPedido);

        public async Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido)
            => await _pedidoRepository.BuscarPorStatus(statusPedido);

        public async Task<Pedido> Cadastrar(int? clienteId, List<ItemPedido> itensPedido)
        {
            var pedido = new Pedido(clienteId, itensPedido);

            pedido = await _pedidoRepository.Cadastrar(pedido);
            await _pedidoRepository.UnitOfWork.Commit();

            var pagamento = await _pagamentoService.RealizarPagamento(pedido.Id, FormaPagamento.QrCodeMercadoPago, pedido.Valor);

            if (pagamento) return pedido;

            else throw new DomainException("Pagamento não autorizado!");
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
