using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Gateways
{
    public class PagamentoGateway : IPagamentoGateway
    {
        private readonly IPagamentoRepository _pagamentoRepository;

        public PagamentoGateway(IPagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        public Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId)
        {
            return _pagamentoRepository.BuscarPagamentoPorPedidoId(pedidoId);
        }

        public Task<Pagamento> Cadastrar(Pagamento pagamento)
        {
            return _pagamentoRepository.Cadastrar(pagamento);
        }

        public async Task CommitAsync()
        {
            await _pagamentoRepository.UnitOfWork.CommitAsync();
        }
    }
}
