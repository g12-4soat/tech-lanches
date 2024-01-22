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

        public Task<Cliente> BuscarPorCpf(Cpf cpf)
        {
            return _clienteRepository.BuscarPorCpf(cpf);
        }

        public Task<Cliente> BuscarPorId(int idCliente)
        {
           return _clienteRepository.BuscarPorId(idCliente);
        }

        public Task<Cliente> Cadastrar(Cliente cliente)
        {
            return _clienteRepository.Cadastrar(cliente);
        }

        public async Task CommitAsync()
        {
            await _clienteRepository.UnitOfWork.CommitAsync();
        }
    }
}