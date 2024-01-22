using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Provedores.MercadoPago;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Pagamentos;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Controllers
{
    public class PagamentoController : IPagamentoController
    {
        private readonly IPagamentoGateway _pagamentoGateway;
        private readonly IPagamentoPresenter _pagamentoPresenter;
        private readonly IServiceProvider _serviceProvider;

        private static readonly string UsuarioId = AppSettings.Configuration.GetSection($"ApiMercadoPago:UserId").Value;
        private static readonly string PosId = AppSettings.Configuration.GetSection($"ApiMercadoPago:PosId").Value;

        public PagamentoController(IPagamentoGateway pagamentoGateway, IPagamentoPresenter pagamentoPresenter, IServiceProvider serviceProvider)
        {
            _pagamentoGateway = pagamentoGateway;
            _pagamentoPresenter = pagamentoPresenter;
            _serviceProvider = serviceProvider;
        }

        public async Task<PagamentoResponseDTO> BuscarPagamentoPorPedidoId(int pedidoId)
        {
            var pagamento = await _pagamentoGateway.BuscarPagamentoPorPedidoId(pedidoId);

            return _pagamentoPresenter.ParaDto(pagamento);
        }

        public async Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor)
        {
            await PagamentoUseCase.Cadastrar(pedidoId, formaPagamento, valor, _pagamentoGateway);
            await _pagamentoGateway.CommitAsync();
        }

        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial)
            => await GetACLService(false).ConsultarPagamento(pedidoComercial);

        public async Task<PagamentoResponseACLDTO> ConsultarPagamentoMockado(string pedidoComercial) 
            => await GetACLService(true).ConsultarPagamento(pedidoComercial);

        public async Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago)
        {
            var pedido = JsonSerializer.Serialize(pedidoMercadoPago);

            var resultado = await GetACLService(false).GerarPagamentoEQrCode(pedido, UsuarioId, PosId);

            return resultado;
        }

        public async Task<string> GerarPagamentoEQrCodeMockado(PedidoACLDTO pedidoMercadoPago)
        {
            var pedido = JsonSerializer.Serialize(pedidoMercadoPago);

            var resultado = await GetACLService(true).GerarPagamentoEQrCode(pedido, UsuarioId, PosId);

            return resultado;
        }

        public async Task<bool> RealizarPagamento(int pedidoId, StatusPagamentoEnum statusPagamento)
        {
            var pagamento = await PagamentoUseCase.RealizarPagamento(pedidoId, statusPagamento, _pagamentoGateway);
            await _pagamentoGateway.CommitAsync();

            return pagamento.StatusPagamento == StatusPagamento.Aprovado;
        }

        private IPagamentoACLService GetACLService(bool isMockado)
        {
            if (isMockado)
                return _serviceProvider.GetRequiredService(typeof(IMercadoPagoMockadoService)) as IPagamentoACLService;

            return _serviceProvider.GetService(typeof(IMercadoPagoService)) as IPagamentoACLService;
        }
    }
}
