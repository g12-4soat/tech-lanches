namespace TechLanches.Application.DTOs
{
    /// <summary>
    /// Schema utilizado para envio de dados do checkout.
    /// </summary>
    public class CheckoutResponseDTO
    {
        /// <summary>
        /// Id do pedido
        /// </summary>
        /// <example>1</example>
        public int PedidoId { get; set; }

        /// <summary>
        /// QRCode de pagamento
        /// </summary>
        /// <example>qrcodedata</example>
        public string QRCodeData { get; set; }
    }
}