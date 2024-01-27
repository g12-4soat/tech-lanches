using TechLanches.Core;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> BuscarPorCpf(Cpf cpf);
        Task<Cliente> Cadastrar(Cliente cliente);
        Task<Cliente> BuscarPorId(int idCliente);
    }
}