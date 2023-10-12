using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Validations
{
    public interface IStatusPedidoValidacao
    {
        void Validar(StatusPedido statusPedidoAtual, StatusPedido statusPedidoNovo);
    }
}
