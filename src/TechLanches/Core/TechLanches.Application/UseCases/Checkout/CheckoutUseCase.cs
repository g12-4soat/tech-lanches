using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Application.UseCases.Pagamentos
{
    public class CheckoutUseCase
    {
        public static async Task<bool> ValidarPedidoCompleto(int pedidoId, IPedidoGateway pedidoGateway)
        {
            var pedido = await pedidoGateway.BuscarPorId(pedidoId)
                ?? throw new DomainException($"Pedido não encontrado para checkout - PedidoId: {pedidoId}");

            if (pedido.StatusPedido != StatusPedido.PedidoCriado)
                throw new DomainException($"Status não autorizado para checkout - StatusPedido: {pedido.StatusPedido}");

            if (pedido.Pagamentos.Count() > 0)
                throw new DomainException($"Pedido já contém pagamento - StatusPagamento: {pedido.Pagamentos?.FirstOrDefault()?.StatusPagamento}");

            return true;
        }
    }
}
