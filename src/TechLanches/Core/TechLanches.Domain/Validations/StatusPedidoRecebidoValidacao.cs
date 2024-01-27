using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoRecebidoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoRecebido;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoCriado || statusPedido == StatusPedido.PedidoCanceladoPorPagamentoRecusado;
    }
}