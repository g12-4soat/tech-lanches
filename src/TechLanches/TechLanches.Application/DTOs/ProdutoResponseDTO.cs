using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.DTOs
{
    public class ProdutoResponseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get;  set; }
        public double Preco { get;  set; }
        public string Categoria { get;  set; }
    }
}