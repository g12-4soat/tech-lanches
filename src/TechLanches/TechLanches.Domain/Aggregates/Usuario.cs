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
            
            Nome = nome;
            Email = new Email(email);
        }

        public string Nome { get; private set; }
        public Email Email { get; private set; }
    }
}