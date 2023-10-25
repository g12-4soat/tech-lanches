using TechLanches.Core;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Domain.ValueObjects
{
    public class ItemPedido : ValueObject
    {
        public ItemPedido(int produtoId, int quantidade, decimal precoProduto)
        {
            ProdutoId = produtoId;
            Quantidade = quantidade;
            PrecoProduto = precoProduto;
            CalcularValor();
            Validar();
        }

        public int PedidoId { get; private set; }
        public int ProdutoId { get; private set; }
        public int Quantidade { get; private set; }
        public decimal PrecoProduto { get; private set; }
        public decimal Valor { get; private set; }
        public Pedido Pedido { get; private set; }
        public Produto Produto { get; private set; }

        private void CalcularValor()
        {
            Valor = Quantidade * PrecoProduto;
        }

        private void Validar()
        {
            if (Quantidade <= 0)
                throw new DomainException("Quantidade deve ser maior que zero.");

            if (PrecoProduto <= 0)
                throw new DomainException("Preço Produto deve ser maior que zero.");

            if (Valor <= 0)
                throw new DomainException("Valor deve ser maior que zero.");
        }

        protected override IEnumerable<object> RetornarPropriedadesDeEquidade()
        {
            yield return ProdutoId;
            yield return Quantidade;
            yield return PrecoProduto;
            yield return Valor;
        }
    }
}