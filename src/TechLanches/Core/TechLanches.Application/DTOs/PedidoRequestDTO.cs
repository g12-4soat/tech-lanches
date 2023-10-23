using System.Text.Json.Serialization;
using TechLanches.Domain.Enums;

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
        public int Id { get; set; }

        public int? ClienteId { get; set; }

        public StatusPedido StatusPedido { get; set; }

        public decimal Valor { get; set; }

        public List<ItemPedidoResponseDTO> ItensPedido { get; set; }
    }

    public class ItemPedidoResponseDTO
    {
        public int ProdutoId { get; set; }

        public string NomeProduto { get; set; }

        public int Quantidade { get; set; }

        public decimal PrecoProduto { get; set; }

        [JsonPropertyName("Valor")]
        public decimal Valor { get; set; }
    }
}
