namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para buscar pagamentos.
    /// </summary>
    public class PagamentoResponseDTO
    {
        /// <summary>
        /// Id Pedido
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Valor Pagamento
        /// </summary>
        /// <example>15.50</example>
        public decimal Valor { get; set; }

        /// <summary>
        /// Valor Pagamento
        /// </summary>
        /// <example>Aprovado</example>
        public string StatusPagamento { get; set; }
    }
}
