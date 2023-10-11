using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Aggregates
{
    public class Pedido : Entity, IAggregateRoot
    {
        private Pedido() { }

        public Pedido(int clienteId, int statusPedidoId, List<ItemPedido> itensPedido)
        {
            ClienteId = clienteId;
            StatusPedido = StatusPedido.From(statusPedidoId);
            _itensPedido = new List<ItemPedido>();
            AdicionarItensPedidos(itensPedido);
            Validar();
        }

        private readonly List<ItemPedido> _itensPedido;
        public IReadOnlyCollection<ItemPedido> ItensPedido => _itensPedido;
        public int ClienteId { get; private set; }
        public decimal Valor { get; private set; }
        public StatusPedido StatusPedido { get; private set; }
        public Cliente Cliente { get; private set; }

        private void AdicionarItensPedidos(List<ItemPedido> itensPedido)
        {
            foreach (var itemPedido in itensPedido)
                AdicionarItemPedido(itemPedido);
        }

        public void AdicionarItemPedido(ItemPedido itemPedido) 
        { 
            _itensPedido.Add(itemPedido);
        }

        private void Validar()
        {
            if (_itensPedido is null || !_itensPedido.Any())
                throw new DomainException("O pedido deve possuir pelo menos um item.");
        }
    }
}
