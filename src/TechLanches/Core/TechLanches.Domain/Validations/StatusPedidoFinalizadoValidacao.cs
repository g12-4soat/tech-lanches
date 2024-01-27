using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoFinalizadoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoFinalizado;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoRetirado;
    }
}
