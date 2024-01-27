using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Validations
{
    public interface IStatusPedidoValidacao
    {
        StatusPedido StatusPedido { get; }
        bool Validar(StatusPedido statusPedido);
    }
}
