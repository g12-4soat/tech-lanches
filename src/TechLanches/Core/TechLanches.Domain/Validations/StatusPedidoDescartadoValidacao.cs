using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoDescartadoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoDescartado;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoPronto;
    }
}
