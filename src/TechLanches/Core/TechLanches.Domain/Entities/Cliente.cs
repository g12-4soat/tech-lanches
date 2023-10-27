using TechLanches.Domain.Aggregates;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Entities
{
    public class Cliente : Usuario
    {
        private Cliente()
        {

        }

        public Cliente(
            string nome,
            string email,
            string cpf) : base(nome, email)
        {
            CPF = new Cpf(cpf);
        }

        public Cpf CPF { get; private set; }

        public IReadOnlyCollection<Pedido> Pedidos { get; private set; }
    }
}