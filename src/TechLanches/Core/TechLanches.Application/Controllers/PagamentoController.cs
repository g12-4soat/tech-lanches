using Microsoft.Extensions.Options;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways;
using TechLanches.Application.Options;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Pagamentos;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Controllers
{
    public class PagamentoController : IPagamentoController
    {
        private readonly IPagamentoPresenter _pagamentoPresenter;
        private readonly IPagamentoRepository _pagamentoRepository;
        private readonly IMercadoPagoMockadoService _mercadoPagoMockadoService;
        private readonly IMercadoPagoService _mercadoPagoService;
        private readonly ApplicationOptions _applicationOptions;
        public PagamentoController(
            IPagamentoRepository pagamentoRepository,
            IPagamentoPresenter pagamentoPresenter,
            IMercadoPagoMockadoService mercadoPagoMockadoService,
            IMercadoPagoService mercadoPagoService,
            IOptions<ApplicationOptions> applicationOptions)
        {
            _pagamentoRepository = pagamentoRepository;
            _pagamentoPresenter = pagamentoPresenter;
            _mercadoPagoMockadoService = mercadoPagoMockadoService;
            _mercadoPagoService = mercadoPagoService;
            _applicationOptions = applicationOptions.Value;
        }

        public async Task<PagamentoResponseDTO> BuscarPagamentoPorPedidoId(int pedidoId)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, false);
            var pagamento = await pagamentoGateway.BuscarPagamentoPorPedidoId(pedidoId);

            return _pagamentoPresenter.ParaDto(pagamento);
        }

        public async Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, false);
            await PagamentoUseCase.Cadastrar(pedidoId, formaPagamento, valor, pagamentoGateway);
            await pagamentoGateway.CommitAsync();
        }

        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, false);
            return await pagamentoGateway.ConsultarPagamentoMercadoPago(pedidoComercial);
        }

        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMockado(string pedidoComercial)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, true);
            return await pagamentoGateway.ConsultarPagamentoMercadoPago(pedidoComercial);
        }

        public async Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, false);
            var resultado = await pagamentoGateway.GerarPagamentoEQrCodeMercadoPago(pedidoMercadoPago);

            return resultado;
        }

        public async Task<string> GerarPagamentoEQrCodeMockado(PedidoACLDTO pedidoMercadoPago)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, true);
            var resultado = await pagamentoGateway.GerarPagamentoEQrCodeMercadoPago(pedidoMercadoPago);

            return resultado;
        }

        public async Task<bool> RealizarPagamento(int pedidoId, StatusPagamentoEnum statusPagamento)
        {
            var pagamentoGateway = new PagamentoGateway(_pagamentoRepository, _mercadoPagoMockadoService, _mercadoPagoService, _applicationOptions, false);
            var pagamento = await PagamentoUseCase.RealizarPagamento(pedidoId, statusPagamento, pagamentoGateway);
            await pagamentoGateway.CommitAsync();

            return pagamento.StatusPagamento == StatusPagamento.Aprovado;
        }
    }
}
