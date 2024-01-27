using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoRetiradoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoRetirado;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoPronto;
    }
}
