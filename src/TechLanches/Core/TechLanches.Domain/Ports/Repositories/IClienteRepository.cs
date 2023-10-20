using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Domain.Ports.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> BuscarPorCpf(Cpf cpf);
        Task<Cliente> Cadastrar(Cliente cliente);
    }
}