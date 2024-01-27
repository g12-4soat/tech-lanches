using System.Text.Json.Serialization;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.DTOs
{
    public record PedidoACLDTO
    {
        [JsonPropertyName("external_reference")]
        public string ReferenciaExterna { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalTransacao { get; set; }

        [JsonPropertyName("items")]
        public List<ItemPedidoACLDTO> ItensPedido { get; set; }

        [JsonPropertyName("title")]
        public string Titulo { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; }

        [JsonPropertyName("expiration_date")]
        public string DataExpiracao { get; set; }

        [JsonPropertyName("notification_url")]
        public string UrlNotificacao { get; set; }
    }

    public record ItemPedidoACLDTO
    {
        [JsonPropertyName("category")]
        public string Categoria { get; set; }

        [JsonPropertyName("title")]
        public string NomeProduto { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantidade { get; set; }

        [JsonPropertyName("unit_measure")]
        public string UnidadeMedida { get; set; }

        [JsonPropertyName("unit_price")]
        public decimal PrecoProduto { get; set; }

        [JsonPropertyName("total_amount")]
        public decimal TotalTransacao { get; set; }
    }
}