using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Aggregates
{
    public class Pedido : Entity, IAggregateRoot
    {
        private Pedido() { }

        public Pedido(int? clienteId, List<ItemPedido> itensPedido)
        {
            ClienteId = clienteId;
            StatusPedido = StatusPedido.PedidoCriado;
            _itensPedido = new List<ItemPedido>();
            AdicionarItensPedidos(itensPedido);
            Validar();
        }

        private readonly List<ItemPedido> _itensPedido;
        public IReadOnlyCollection<ItemPedido> ItensPedido => _itensPedido;
        public int? ClienteId { get; private set; }
        public decimal Valor { get; private set; }
        public StatusPedido StatusPedido { get; private set; }
        public Cliente? Cliente { get; private set; }
        public bool ClienteIdentificado => ClienteId.HasValue;

        public IReadOnlyCollection<Pagamento> Pagamentos { get; private set; }

        private void AdicionarItensPedidos(List<ItemPedido> itensPedido)
        {
            foreach (var itemPedido in itensPedido)
                AdicionarItemPedido(itemPedido);
        }

        public void AdicionarItemPedido(ItemPedido itemPedido)
        {
            _itensPedido.Add(itemPedido);
            CalcularValor();
        }

        private void CalcularValor()
        {
            Valor = _itensPedido.Sum(i => i.Valor);
        }

        public void TrocarStatus(IStatusPedidoValidacaoService validacaoService, StatusPedido statusPedidoNovo)
        {
            if (!Enum.IsDefined(typeof(StatusPedido), statusPedidoNovo))
                throw new DomainException("Status inválido");

            validacaoService.Validar(StatusPedido, statusPedidoNovo);
            StatusPedido = statusPedidoNovo;
        }

        private void Validar()
        {
            ArgumentNullException.ThrowIfNull(_itensPedido);
            if (!_itensPedido.Any())
                throw new DomainException("O pedido deve possuir pelo menos um item.");

            if (Valor <= 0)
                throw new DomainException("Valor deve ser maior que zero.");
        }
    }
}
