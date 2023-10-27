using TechLanches.Core;

namespace TechLanches.Domain.ValueObjects
{
    public class CategoriaProduto : Enumeration
    {
        public static CategoriaProduto Lanche = new(1, nameof(Lanche));
        public static CategoriaProduto Acompanhamento = new(2, nameof(Acompanhamento));
        public static CategoriaProduto Bebida = new(3, nameof(Bebida));
        public static CategoriaProduto Sobremesa = new(4, nameof(Sobremesa));
        public CategoriaProduto(int id, string nome)
        : base(id, nome)
        {
        }

        private CategoriaProduto() { }

        public static IEnumerable<CategoriaProduto> List() =>
       new[] { Lanche, Acompanhamento, Bebida, Sobremesa };

        public static CategoriaProduto From(int id)
        {
            var categoria = List().SingleOrDefault(s => s.Id == id);

            if (categoria == null)
            {
                throw new DomainException($"Possiveis valores para categoria: {string.Join(",", List().Select(s => $" {s.Id}-{s.Nome}"))}");
            }

            return categoria;
        }
    }
}
