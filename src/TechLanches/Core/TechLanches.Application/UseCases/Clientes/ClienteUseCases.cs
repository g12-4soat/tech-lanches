using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.UseCases.Clientes
{
    public class ClienteUseCases
    {
        public static async Task<Cliente> Cadastrar(
            string nome, string email, string cpf,
            IClienteGateway clienteGateway)
        {
            var clienteExistente = await clienteGateway.BuscarPorCpf(RetornarCpf(cpf));

            if (clienteExistente is not null)
                throw new DomainException($"Cliente já existente para CPF: {cpf}");

            var cliente = new Cliente(nome, email, cpf);

            var novoCliente = await clienteGateway.Cadastrar(cliente);

            return novoCliente;
        }

        public static async Task<Cliente> IdentificarCliente(
            string cpf,
            IClienteGateway clienteGateway)
        {
            if (cpf is null) return null;
            var clienteExistente = await clienteGateway.BuscarPorCpf(RetornarCpf(cpf));

            if (clienteExistente is null) throw new DomainException("Cliente não cadastrado!");

            return clienteExistente;
        }

        private static Cpf RetornarCpf(string cpf)
        {
            return new Cpf(cpf);
        }
    }
}