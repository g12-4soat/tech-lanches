using System.Text.Json;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Options;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Application.Gateways
{
    public class PagamentoGateway : IPagamentoGateway
    {
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMercadoPagoMockadoService _mercadoPagoMockadoService;
        private readonly IMercadoPagoService _mercadoPagoService;
        private readonly ApplicationOptions _applicationOptions;
        private readonly bool _mockado;

        public PagamentoGateway(
            IPagamentoRepository pagamentoRepository, 
            IMercadoPagoMockadoService mercadoPagoMockadoService, 
            IMercadoPagoService mercadoPagoService,
            ApplicationOptions applicationOptions,
            bool mockado)
        {
            _pagamentoRepository = pagamentoRepository;
            _mercadoPagoMockadoService = mercadoPagoMockadoService;
            _mercadoPagoService = mercadoPagoService;
            _applicationOptions = applicationOptions;
            _mockado = mockado;
        }

        public Task<Pagamento> BuscarPagamentoPorPedidoId(int pedidoId)
        {
            return _pagamentoRepository.BuscarPagamentoPorPedidoId(pedidoId);
        }

        public Task<Pagamento> Cadastrar(Pagamento pagamento)
        {
            throw new NotImplementedException();
        }

        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial)
        {
            if (_mockado)
            {
                return await _mercadoPagoMockadoService.ConsultarPagamento(pedidoComercial);
            }

            return await _mercadoPagoService.ConsultarPagamento(pedidoComercial);
        }

        public async Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago)
        {
            var pedido = JsonSerializer.Serialize(pedidoMercadoPago);

            if (_mockado)
            {
                return await _mercadoPagoMockadoService.GerarPagamentoEQrCode(pedido, _applicationOptions.UserId, _applicationOptions.PosId);
            }

            return await _mercadoPagoService.GerarPagamentoEQrCode(pedido, _applicationOptions.UserId, _applicationOptions.PosId);
        }

        public async Task CommitAsync()
        {
            await _pagamentoRepository.UnitOfWork.CommitAsync();
        }
    }
}
