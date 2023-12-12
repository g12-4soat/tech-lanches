using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoProntoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoPronto;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoEmPreparacao;
    }
}
