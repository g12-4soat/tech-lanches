using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services;
using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> BuscarPorCpf(string cpf)
        {
            var documento = RetornarCpf(cpf);

            return await _clienteRepository.BuscarPorCpf(documento);
        }

        public async Task<Cliente> Cadastrar(string nome, string email, string cpf)
        {
            var documento = RetornarCpf(cpf);

            var clienteExistente = await _clienteRepository.BuscarPorCpf(documento);

            if (clienteExistente is not null)
                throw new DomainException($"Cliente já existente para CPF: {cpf}");

            var cliente = new Cliente(nome, email, cpf);

            var novoCliente = await _clienteRepository.Cadastrar(cliente);

            await _clienteRepository.UnitOfWork.Commit();

            return novoCliente;
        }

        private static Cpf RetornarCpf(string cpf)
        {
            return new Cpf(cpf);
        }
    }
}