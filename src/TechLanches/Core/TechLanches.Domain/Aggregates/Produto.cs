using TechLanches.Core;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Aggregates
{
    public class Produto : Entity, IAggregateRoot
    {
        protected Produto() { }

        public Produto(string nome, string descricao, decimal preco, int categoriaId)
        {
            Atualizar(nome, descricao, preco, categoriaId);
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public CategoriaProduto Categoria { get; private set; }
        public bool Deletado { get; private set; }
        public IReadOnlyCollection<ItemPedido> ItensPedidos { get; private set; }


        private void Validar()
        {
            ArgumentNullException.ThrowIfNull(Nome);

            if (Nome == string.Empty)
                throw new DomainException("O nome não pode ser nulo.");

            if (Nome.Length > 100)
                throw new DomainException("Nome deve possuir no máximo 100 caracteres.");

            if (Nome.Length < 3)
                throw new DomainException("Nome deve possuir no mínimo 3 caracteres.");

            ArgumentNullException.ThrowIfNull(Descricao);
            if (Descricao.Length > 300)
                throw new DomainException("Descrição deve possuir no máximo 300 caracteres.");

            if (Descricao.Length < 10)
                throw new DomainException("Descrição deve possuir no mínimo 10 caracteres.");

            ArgumentNullException.ThrowIfNull(Preco);
            if (Preco <= 0)
                throw new DomainException("Preço deve ser maior do que 0.");
        }

        public void Atualizar(string nome, string descricao, decimal preco, int categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Categoria = CategoriaProduto.From(categoriaId);

            Validar();
        }

        public void DeletarProduto()
        {
            Deletado = true;
        }
    }
}
