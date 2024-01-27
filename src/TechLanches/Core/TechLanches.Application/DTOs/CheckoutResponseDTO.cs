using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        /// Código de pagamento
        /// </summary>
        /// <example>qrcodedata</example>
        
        public string QRCodeData { get; set; }
        /// <summary>
        /// URL para exibir o QrCode de pagamento
        /// </summary>
        /// <example>https://api.qrserver.com/v1/create-qr-code/?size=1500x1500&data=qrcodeexemplo</example>
        public string URLData { get; set; }

        [JsonIgnore]
        public byte[] QRCodeImage { get; set; }

        [JsonIgnore]
        public string ResultType { get; set; }
    }
}