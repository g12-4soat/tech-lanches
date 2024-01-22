namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para envio de dados do pedido.
    /// </summary>
    public class PedidoRequestDTO
    {
        /// <summary>
        /// CPF do cliente
        /// </summary>
        /// <example>510.138.370-88</example>
        public string? Cpf { get; set; }
        public List<ItemPedidoRequestDTO> ItensPedido { get; set; } 
    }

    /// <summary>
    /// Schema utilizado para envio de dados do itens pedido.
    /// </summary>
    public class ItemPedidoRequestDTO
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        /// <example>1</example>
        public int IdProduto { get; set; }

        /// <summary>
        /// Quantidade do item
        /// </summary>
        /// <example>2</example>
        public int Quantidade { get; set; }
    }
}
