using TechLanches.Core;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoRetiradoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoRetirado && statusPedidoAtual != StatusPedido.PedidoPronto)
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
