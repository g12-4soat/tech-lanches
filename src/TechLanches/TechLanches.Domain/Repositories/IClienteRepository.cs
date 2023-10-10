using TechLanches.Domain.Entities;

namespace TechLanches.Domain.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> BuscarPorCpf(string cpf);
        Task<Cliente> Cadastrar(Cliente cliente);
    }
}