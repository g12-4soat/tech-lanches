using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Validations;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Aggregates
{
    public class Pedido : Entity, IAggregateRoot
    {
        private Pedido() { }

        public Pedido(int clienteId, List<ItemPedido> itensPedido)
        {
            ClienteId = clienteId;
            StatusPedido = StatusPedido.PedidoCriado;
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
            CalcularValor();
        }

        private void CalcularValor()
        {
            Valor = _itensPedido.Sum(i => i.Valor);
        }

        public void TrocarStatus(StatusPedido statusPedido)
        {
            ValidarStatus(statusPedido);
            StatusPedido = statusPedido;
        }

        private void ValidarStatus(StatusPedido statusPedidoNovo)
        {
            new StatusPedidoValidacao(new StatusPedidoCriadoValidacao()).Validar(StatusPedido, statusPedidoNovo);
            new StatusPedidoValidacao(new StatusPedidoEmPreparacaoValidacao()).Validar(StatusPedido, statusPedidoNovo);
            new StatusPedidoValidacao(new StatusPedidoProntoValidacao()).Validar(StatusPedido, statusPedidoNovo);
            new StatusPedidoValidacao(new StatusPedidoRetiradoValidacao()).Validar(StatusPedido, statusPedidoNovo);
            new StatusPedidoValidacao(new StatusPedidoDescartadoValidacao()).Validar(StatusPedido, statusPedidoNovo);
            new StatusPedidoValidacao(new StatusPedidoCanceladoValidacao()).Validar(StatusPedido, statusPedidoNovo);
            new StatusPedidoValidacao(new StatusPedidoFinalizadoValidacao()).Validar(StatusPedido, statusPedidoNovo);
        }

        private void Validar()
        {
            if (_itensPedido is null || !_itensPedido.Any())
                throw new DomainException("O pedido deve possuir pelo menos um item.");

            if (Valor <= 0)
                throw new DomainException("Valor deve ser maior que zero.");
        }
    }
}
