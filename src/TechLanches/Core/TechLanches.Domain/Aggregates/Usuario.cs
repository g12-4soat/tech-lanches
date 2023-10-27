using TechLanches.Core;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Aggregates
{
    public abstract class Usuario : Entity, IAggregateRoot
    {
        protected Usuario() { }

        protected Usuario(string nome, string email)
        {
            ArgumentNullException.ThrowIfNull(nome);

            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException($"Nome não pode ser nulo ou vazio");

            Nome = nome;
            Email = new Email(email);

            Validar();
        }

        public string Nome { get; private set; }
        public Email Email { get; private set; }

        private void Validar()
        {
            if (Nome.Length <= 1 || Nome.Length > 50)
                throw new DomainException($"Nome precisa ter entre 2 e 50 caracteres");
        }
    }
}