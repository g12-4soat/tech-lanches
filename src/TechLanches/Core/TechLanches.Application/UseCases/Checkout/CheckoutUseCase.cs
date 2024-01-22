using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs;
using TechLanches.Adapter.ACL.Pagamento.QrCode.Models;
using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.UseCases.Pagamentos
{
    public class CheckoutUseCase
    {
        public static async Task<bool> ValidarPedidoCompleto(int pedidoId, IPedidoController pedidoController)
        {
            var pedido = await pedidoController.BuscarPorId(pedidoId)
                ?? throw new DomainException($"Pedido não encontrado para checkout - PedidoId: {pedidoId}");

            if (pedido.StatusPedido != StatusPedido.PedidoCriado)
                throw new DomainException($"Status não autorizado para checkout - StatusPedido: {pedido.StatusPedido}");

            if (pedido.Pagamentos is not null)
                throw new DomainException($"Pedido já contém pagamento - StatusPagamento: {pedido.Pagamentos?.FirstOrDefault()?.StatusPagamento}");

            return true;
        }
    }
}
