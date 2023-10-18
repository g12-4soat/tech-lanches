using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoDescartadoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoDescartado && statusPedidoAtual != StatusPedido.PedidoPronto)
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
