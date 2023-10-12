using TechLanches.Core;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoCriadoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoCriado && statusPedidoAtual != StatusPedido.PedidoCriado)
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
