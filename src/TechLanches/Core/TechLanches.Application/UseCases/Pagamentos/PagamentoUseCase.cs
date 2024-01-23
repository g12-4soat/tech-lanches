using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Models;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.UseCases.Pagamentos
{
    public class PagamentoUseCase
    {
        public static async Task Cadastrar(int pedidoId, FormaPagamento formaPagamento, decimal valor, IPagamentoGateway pagamentoGateway)
        {
            await VerificarPagamentoExistente(pedidoId, pagamentoGateway);

            Pagamento pagamento = new(pedidoId, valor, formaPagamento);
            await pagamentoGateway.Cadastrar(pagamento);
        }

        public static async Task<Pagamento> RealizarPagamento(int pedidoId, StatusPagamentoEnum statusPagamento, IPagamentoGateway pagamentoGateway)
        {
            var pagamento = await VerificarPagamentoExistente(pedidoId, pagamentoGateway);

            if (statusPagamento == StatusPagamentoEnum.Aprovado)
                pagamento.Aprovar();
            else
                pagamento.Reprovar();

            return pagamento;
        }

        //private static async Task<Pagamento> VerificarPagamentoExistente(int id, IPagamentoGateway pagamentoGateway)
        //{
        //    var pagamento = await pagamentoGateway.BuscarPagamentoPorPedidoId(id);

        //    if (pagamento is not null)
        //        throw new DomainException($"Pagamento já cadastrado para o pedido: {id} com status: {pagamento.StatusPagamento}.");

        //    return pagamento;
        //}
        private static async Task<Pagamento> VerificarPagamentoExistente(int id, IPagamentoGateway pagamentoGateway)
        {
            var pagamento = await pagamentoGateway.BuscarPagamentoPorPedidoId(id)
                ?? throw new DomainException($"Pagamento já efetuado para o pedido: {id}.");

            return pagamento;
        }
    }
}
