using System.Text.Json.Serialization;

namespace TechLanches.Adapter.ACL.Pagamento.QrCode.Models
{
    public class PedidoComercial
    {
        [JsonPropertyName("elements")]
        public List<Pedido> Elementos { get; set; }
    }

    public class Pedido
    {
        [JsonPropertyName("id")]
        public decimal Id { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("external_reference")]
        public string ExternalReference { get; set; }

        [JsonPropertyName("preference_id")]
        public string PreferenceId { get; set; }

        [JsonPropertyName("payments")]
        public List<PagamentoGerado> Pagamentos { get; set; }

        [JsonPropertyName("notification_url")]
        public string UrlNotificacao { get; set; }

        [JsonPropertyName("date_created")]
        public string DataCriacao { get; set; }

        [JsonPropertyName("last_updated")]
        public string UltimaAtualizacao { get; set; }

        [JsonPropertyName("items")]
        public List<Items> Items { get; set; }
    }

    public class PagamentoGerado
    {
        [JsonPropertyName("id")]
        public decimal Id { get; set; }

        [JsonPropertyName("transaction_amount")]
        public decimal ValorTransacao { get; set; }

        [JsonPropertyName("total_paid_amount")]
        public decimal TotalPagoTransacao { get; set; }

        [JsonPropertyName("shipping_cost")]
        public int Frete { get; set; }

        [JsonPropertyName("currency_id")]
        public string MoedaId { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("status_detail")]
        public string StatusDetalhes { get; set; }

        [JsonPropertyName("operation_type")]
        public string TypeOperacao { get; set; }

        [JsonPropertyName("date_approved")]
        public string DataAprovacao { get; set; }

        [JsonPropertyName("date_created")]
        public string DataCriacao { get; set; }

        [JsonPropertyName("last_modified")]
        public string UltimaModificacao { get; set; }

        [JsonPropertyName("amount_refunded")]
        public decimal ValorReembolsado { get; set; }
    }

    public class Items
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("category_id")]
        public string CategoriaId { get; set; }

        [JsonPropertyName("currency_id")]
        public string MoedaId { get; set; }

        [JsonPropertyName("description")]
        public string Descricao { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantidade { get; set; }

        [JsonPropertyName("unit_price")]
        public int PrecoUnitario { get; set; }
    }

    public class PedidoGerado
    {
        [JsonPropertyName("in_store_order_id")]
        public string in_store_order_id { get; set; }

        [JsonPropertyName("qr_data")]
        public string qr_data { get; set; }
    }
}
