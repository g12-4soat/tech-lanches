namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para envio de dados do produto.
    /// </summary>
    public class ProdutoRequestDTO
    {
        /// <summary>
        /// Nome do produto
        /// </summary>
        /// <example>X-Burguer</example>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        /// <example>Lanche com pão carne e queijo</example>
        public string Descricao { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <example>20</example>
        public decimal Preco { get; set; }

        /// <summary>
        /// Categoria do produto
        /// </summary>
        /// <example>1</example>
        public int CategoriaId { get; set; }
    }
}