using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoCanceladoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoCancelado;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoCriado;
    }
}
