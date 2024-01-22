using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Application.DTOs;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.Controllers.Interfaces
{
    public interface IPagamentoController
    {
        Task<bool> RealizarPagamento(int pedidoId, StatusPagamentoEnum statusPagamento);
        Task<PagamentoResponseDTO> BuscarPagamentoPorPedidoId(int pedidoId);
        Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor);
        Task<string> GerarPagamentoEQrCodeMercadoPago(PedidoACLDTO pedidoMercadoPago);
        Task<PagamentoResponseACLDTO> ConsultarPagamentoMercadoPago(string pedidoComercial);
        Task<PagamentoResponseACLDTO> ConsultarPagamentoMockado(string pedidoComercial);
        Task<string> GerarPagamentoEQrCodeMockado(PedidoACLDTO pedidoMercadoPago);
    }
}
