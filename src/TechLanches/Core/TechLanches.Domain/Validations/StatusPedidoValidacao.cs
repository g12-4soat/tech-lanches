using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public class StatusPedidoValidacao
    {
        private readonly IStatusPedidoValidacao _statusPedidoValidacao;

        public StatusPedidoValidacao(IStatusPedidoValidacao statusPedidoValidacao)
        {
            _statusPedidoValidacao = statusPedidoValidacao;
        }

        public void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo)
        {
            _statusPedidoValidacao.Validar(statusPedidoAtual, statusPedidoNovo);
        }
    }
}
