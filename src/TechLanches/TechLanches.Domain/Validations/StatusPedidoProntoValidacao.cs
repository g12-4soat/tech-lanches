using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoProntoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoPronto && statusPedidoAtual != StatusPedido.PedidoEmPreparacao)
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
