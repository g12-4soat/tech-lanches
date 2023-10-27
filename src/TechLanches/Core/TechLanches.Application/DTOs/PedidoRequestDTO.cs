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

    /// <summary>
    /// Schema utilizado para retorno de dados do pedido.
    /// </summary>
    public class PedidoResponseDTO
    {
        /// <summary>
        /// Id do pedido
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Id do cliente
        /// </summary>
        /// <example>2</example>
        public int? ClienteId { get; set; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        /// <example>Ana Luiza</example>
        public string NomeCliente { get; set; }

        /// <summary>
        /// Status do pedido
        /// </summary>
        /// <example>PedidoCriado</example>
        public string StatusPedido { get; set; }

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        /// <example>12</example>
        public decimal Valor { get; set; }

        public List<ItemPedidoResponseDTO> ItensPedido { get; set; }
    }

    /// <summary>
    /// Schema utilizado para retorno de dados do itens pedido.
    /// </summary>
    public class ItemPedidoResponseDTO
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        /// <example>1</example>
        public int ProdutoId { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        /// <example>X-Burguer</example>
        public string NomeProduto { get; set; }

        /// <summary>
        /// Quantidade do item
        /// </summary>
        /// <example>2</example>
        public int Quantidade { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <example>8</example>
        public decimal PrecoProduto { get; set; }

        /// <summary>
        /// Valor total do item
        /// </summary>
        /// <example>16</example>
        public decimal Valor { get; set; }
    }
}
