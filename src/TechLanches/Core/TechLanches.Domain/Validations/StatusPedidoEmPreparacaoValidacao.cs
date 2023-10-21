using TechLanches.Core;
using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoEmPreparacaoValidacao : IStatusPedidoValidacao
    {
        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            if (statusPedidoNovo == StatusPedido.PedidoEmPreparacao 
                && (statusPedidoAtual != StatusPedido.PedidoCriado 
                    && statusPedidoAtual != StatusPedido.PedidoEmPreparacao))
            {
                throw new DomainException("O status selecionado não é válido");
            }
        }
    }
}
