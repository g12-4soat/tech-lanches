using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Entities;

namespace TechLanches.Application.UseCases.Clientes
{
    public class ClienteUseCases
    {
        public static async Task<Cliente> Cadastrar(
            string nome, string email, string cpf,
            IClienteGateway clienteGateway)
        {
            var clienteExistente = await clienteGateway.BuscarPorCpf(cpf);

            if (clienteExistente is not null)
                throw new DomainException($"Cliente já existente para CPF: {cpf}");

            var novoCliente = await clienteGateway.Cadastrar(nome,email,cpf);
            return novoCliente;
        }

        public static async Task<Cliente> BuscarPorCpf(
            string cpf,
            IClienteGateway clienteGateway)
        {
            var clienteExistente = await clienteGateway.BuscarPorCpf(cpf);

            if (clienteExistente is null) throw new DomainException("Cliente não cadastrado!");

            return clienteExistente;
        }
    }
}