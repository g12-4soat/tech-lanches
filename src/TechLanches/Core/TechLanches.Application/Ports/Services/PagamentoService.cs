using TechLanches.Adapter.ACL.Pagamento.QrCode;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services
{
    public class PagamentoService : IPagamentoService
    {
        private readonly IPagamentoRepository _repository;
        private readonly IPagamentoQrCodeACLService _serviceACL;

        public PagamentoService(IPagamentoRepository repository, IPagamentoQrCodeACLService serviceACL)
        {
            _repository = repository;
            _serviceACL = serviceACL;
        }

        public async Task Aprovar(Pagamento pagamento)
        {
            _repository.Aprovar(pagamento);
            await _repository.UnitOfWork.Commit();
        }

        public async Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId)
        {
            return await _repository.BuscarStatusPagamentoPorPedidoId(pedidoId);
        }

        public async Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            var pagamentoExistente = await _repository.BuscarStatusPagamentoPorPedidoId(pedidoId);

            if (pagamentoExistente is not null)
                throw new DomainException($"Pagamento já efetuado para o pedido: {pagamentoExistente.Id}.");

            Pagamento pagamento = new(pedidoId, valor, formaPagamento);
            await _repository.Cadastrar(pagamento);
            await _repository.UnitOfWork.Commit();
        }

        public async Task<string> GerarQrCode()
            => await _serviceACL.GerarQrCode();

        public Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            //salvar informação de pagamento
            return Task.FromResult(true);
        }

        public async Task Reprovar(Pagamento pagamento)
        {
            _repository.Reprovar(pagamento);
            await _repository.UnitOfWork.Commit();
        }
    }
}
