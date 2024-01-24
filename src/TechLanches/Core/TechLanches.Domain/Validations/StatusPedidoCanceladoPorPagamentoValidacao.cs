using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoCanceladoPorPagamentoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoCanceladoPorPagamentoRecusado;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoCriado || statusPedido == StatusPedido.PedidoCanceladoPorPagamentoRecusado;
    }
}
