using TechLanches.Domain.Entities;
using TechLanches.Domain.Repositories;
using TechLanches.Domain.Services;
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
            var documento = new Cpf(cpf);

            return await _clienteRepository.BuscarPorCpf(documento.Numero);
        }

        public async Task<Cliente> Cadastrar(string nome, string email, string cpf)
        {
            var cliente = new Cliente(nome, email, cpf);

            var novoCliente = await _clienteRepository.Cadastrar(cliente);

            await _clienteRepository.UnitOfWork.Commit();

            return novoCliente;
        }
    }
}
