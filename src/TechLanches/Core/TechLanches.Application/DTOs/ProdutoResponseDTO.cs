namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para retorno de dados do produto.
    /// </summary>
    public class ProdutoResponseDTO
    {
        /// <summary>
        /// Id do produto
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        /// <example>X-Burguer</example>
        public string Nome { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        /// <example>Lanche com pão carne e queijo</example>
        public string Descricao { get;  set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <example>25</example>
        public decimal Preco { get;  set; }

        /// <summary>
        /// Categoria do produto
        /// </summary>
        /// <example>Lanche</example>
        public string Categoria { get;  set; }

        /// <summary>
        /// Categoria do produto id
        /// </summary>
        /// <example>Lanche</example>
        public int CategoriaId { get; set; }
    }
}