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

            // add Evento ClienteCadastrado
            //AdicionarEventoDominio(new ClienteCadastrado());
        }

        public Cpf CPF { get; private set; }
    }
}