using System.Text.Json.Serialization;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.DTOs
{
    public class PedidoRequestDTO
    {
        public string? Cpf { get; set; }
        public List<ItemPedidoDTO> ItensPedido { get; set; } 
    }

    public class ItemPedidoDTO
    {
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }
    }

    public class PedidoResponseDTO
    {
        [JsonPropertyName("PedidoId")]
        public int Id { get; set; }

        [JsonPropertyName("ClienteId")]
        public int? ClienteId { get; set; }

        [JsonPropertyName("StatusPedido")]
        public StatusPedido StatusPedido { get; set; }

        [JsonPropertyName("ValorTotal")]
        public decimal Valor { get; set; }

        [JsonPropertyName("ItensPedido")]
        public List<ItemPedidoResponseDTO> ItensPedido { get; set; }
    }

    public class ItemPedidoResponseDTO
    {
        [JsonPropertyName("ProdutoId")]
        public int ProdutoId { get; set; }

        [JsonPropertyName("Quantidade")]
        public int Quantidade { get; set; }

        [JsonPropertyName("PrecoProduto")]
        public decimal PrecoProduto { get; set; }

        [JsonPropertyName("Valor")]
        public decimal Valor { get; set; }
    }
}
