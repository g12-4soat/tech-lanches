namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para retorno de dados do itens pedido.
    /// </summary>
    public class ItemPedidoResponseDTO
    {
        /// <summary>
        /// Produto Vinculado
        /// </summary>
        /// <example>8</example>
        public ProdutoResponseDTO Produto { get; set; }

        /// <summary>
        /// Quantidade do item
        /// </summary>
        /// <example>2</example>
        public int Quantidade { get; set; }

        /// <summary>
        /// Valor total do item
        /// </summary>
        /// <example>16</example>
        public decimal Valor { get; set; }
    }
}
