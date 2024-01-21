using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Repositories;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Gateways
{
    public class ClienteGateway : IClienteGateway
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteGateway(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public Task<Cliente> BuscarPorCpf(string cpf)
        {
            var documento = RetornarCpf(cpf);

            return _clienteRepository.BuscarPorCpf(documento);
        }

        public Task<Cliente> BuscarPorId(int idCliente)
        {
           return _clienteRepository.BuscarPorId(idCliente);
        }

        public Task<Cliente> Cadastrar(string nome, string email, string cpf)
        {
            var cliente = new Cliente(nome, email, cpf);
            return _clienteRepository.Cadastrar(cliente);
        }

        public async Task CommitAsync()
        {
            await _clienteRepository.UnitOfWork.CommitAsync();
        }

        private static Cpf RetornarCpf(string cpf)
        {
            return new Cpf(cpf);
        }
    }
}