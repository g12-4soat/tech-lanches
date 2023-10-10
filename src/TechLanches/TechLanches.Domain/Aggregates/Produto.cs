using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLanches.Core;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Aggregates
{
    public class Produto : Entity, IAggregateRoot
    {
        protected Produto() { }

        protected Produto(string nome, string descricao, double preco, int categoriaId)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Categoria = CategoriaProduto.From(categoriaId);

            Validar();
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Preco { get; private set; }
        public CategoriaProduto Categoria { get; private set; }

        private void Validar()
        {
            ArgumentNullException.ThrowIfNull(Nome);
            if (Nome.Length > 100)
                throw new DomainException("Nome deve possuir no máximo 100 caracteres.");

            ArgumentNullException.ThrowIfNull(Descricao);
            if (Descricao.Length > 300)
                throw new DomainException("Descrição deve possuir no máximo 300 caracteres.");

            ArgumentNullException.ThrowIfNull(Preco);
            if (Preco <= 0)
                throw new DomainException("Preço deve ser maior do que 0.");
        }
    }
}
