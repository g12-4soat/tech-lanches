namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para retorno de dados do cliente.
    /// </summary>
    public class ClienteResponseDTO
    {
        /// <summary>
        /// Id do cliente
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        /// <example>Maria da Silva</example>
        public string Nome { get; set; }
        /// <summary>
        /// E-mail do cliente
        /// </summary>
        /// <example>maria.silva@usuario.com</example>
        public string Email { get; set; }
        /// <summary>
        /// CPF do cliente
        /// </summary>
        /// <example>510.138.370-88</example>
        public string CPF { get; set; }
    }
}
