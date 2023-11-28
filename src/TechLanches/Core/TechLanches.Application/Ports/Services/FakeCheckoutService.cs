using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Ports.Services
{
    public class FakeCheckoutService : IPagamentoService
    {
        private readonly IPagamentoRepository _repository;

        public FakeCheckoutService(IPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<Pagamento> BuscarStatusPagamentoPorPedidoId(int pedidoId)
        {
            return await _repository.BuscarStatusPagamentoPorPedidoId(pedidoId);
        }

        public Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            //salvar informação de pagamento
            return Task.FromResult(true);
        }
    }
}
