namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para buscar status pedidos.
    /// </summary>
    public class StatusPedidoResponseDTO
    {
        /// <summary>
        /// Id status pedido
        /// </summary>
        /// <example>1</example>
        public int Id { get; set; }

        /// <summary>
        /// Nome status pedido
        /// </summary>
        /// <example>PedidoEmPreparacao</example>
        public string Nome { get; set; }
    }
}
