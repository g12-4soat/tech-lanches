using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoRecebidoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoRecebido && statusPedidoAtual != StatusPedido.PedidoCriado)
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
