using TechLanches.Domain.Entities;

namespace TechLanches.Domain.Ports.Services
{
    public interface IClienteService
    {
        Task<Cliente> BuscarPorCpf(string cpf);
        Task<Cliente> Cadastrar(string nome, string email, string cpf);
    }
}