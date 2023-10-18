using TechLanches.Domain.Entities;

namespace TechLanches.Domain.Services
{
    public interface IClienteService
    {
        Task<Cliente> BuscarPorCpf(string cpf);
        Task<Cliente> Cadastrar(string nome, string email, string cpf);
    }
}