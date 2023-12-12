using TechLanches.Domain.Enums;

namespace TechLanches.Domain.Services
{
    public interface IStatusPedidoValidacaoService
    {
        void Validar(StatusPedido statusPedido, StatusPedido novoStatusPedido);
    }
}