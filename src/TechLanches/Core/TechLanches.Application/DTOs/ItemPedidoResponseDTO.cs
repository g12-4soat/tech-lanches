namespace TechLanches.Application.DTOs
{
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
