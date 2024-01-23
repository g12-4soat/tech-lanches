using TechLanches.Domain.Enums;

namespace TechLanches.Application.DTOs
{
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
        /// Nome do Status do pedido 
        /// </summary>
        /// <example>PedidoCriado</example>
        public string NomeStatusPedido { get; set; }

        /// <summary>
        /// Status do pedido
        /// </summary>
        /// <example>PedidoCriado</example>
        public StatusPedido StatusPedido { get; set; }

        /// <summary>
        /// Valor total do pedido
        /// </summary>
        /// <example>12</example>
        public decimal Valor { get; set; }

        public List<ItemPedidoResponseDTO> ItensPedido { get; set; }
        public List<PagamentoResponseDTO> Pagamentos { get; set; }
    }
}
