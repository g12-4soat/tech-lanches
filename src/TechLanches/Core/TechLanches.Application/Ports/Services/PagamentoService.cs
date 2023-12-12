using System.Text.Json;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
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
        private readonly IPagamentoACLService _pagamentoACLService;
        private static string UsuarioId = AppSettings.Configuration.GetSection($"ApiMercadoPago:{AppSettings.GetEnv()}")["UserId"];
        private static string PosId = AppSettings.Configuration.GetSection($"ApiMercadoPago:{AppSettings.GetEnv()}")["PosId"];

        public PagamentoService(IPagamentoRepository repository, 
                                IPagamentoACLService pagamentoACLService)
        {
            _repository = repository;
            _pagamentoACLService = pagamentoACLService;
        }

        public async Task Aprovar(Pagamento pagamento)
        {
            _repository.Aprovar(pagamento);
            await _repository.UnitOfWork.Commit();
        }
        public async Task Reprovar(Pagamento pagamento)
        {
            _repository.Reprovar(pagamento);
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

        public Task<bool> RealizarPagamento(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            //salvar informação de pagamento
            return Task.FromResult(true);
        }

        public async Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago)
        {
            var pedido = JsonSerializer.Serialize(pedidoMercadoPago);

            var resultado = await _pagamentoACLService.GerarPagamentoEQrCode(pedido, UsuarioId, PosId);

            return resultado;
        }
         
        //exemplo de pedidoId = 13971205222
        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial) 
            => await _pagamentoACLService.ConsultarPagamento(pedidoComercial);
    }
}
