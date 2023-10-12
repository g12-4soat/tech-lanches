using TechLanches.Core;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoFinalizadoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoFinalizado && statusPedidoAtual != StatusPedido.PedidoRetirado)
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
