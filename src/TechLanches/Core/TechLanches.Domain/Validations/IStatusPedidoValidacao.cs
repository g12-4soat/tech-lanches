using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public interface IStatusPedidoValidacao
    {
        void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo);
    }
}
