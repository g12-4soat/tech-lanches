namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para buscar categorias.
    /// </summary>
    public class CategoriaResponseDTO
    {
        /// <summary>
        /// Id categoria
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome categoria
        /// </summary>
        /// <example>Lanche</example>
        public string Nome { get; set; }
    }
}
