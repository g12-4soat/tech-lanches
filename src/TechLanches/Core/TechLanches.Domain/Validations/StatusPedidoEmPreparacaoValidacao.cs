using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoEmPreparacaoValidacao : IStatusPedidoValidacao
    {
        public StatusPedido StatusPedido => StatusPedido.PedidoEmPreparacao;

        public bool Validar(StatusPedido statusPedido) => statusPedido == StatusPedido.PedidoRecebido;
    }
}
