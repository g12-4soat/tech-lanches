namespace TechLanches.Application.DTOs
{
    public class ProdutoRequestDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int CategoriaId { get; set; }
    }
}