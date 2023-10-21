using TechLanches.Domain.Entities;

namespace TechLanches.Application.Ports.Services
{
    public interface IClienteService
    {
        Task<Cliente> BuscarPorCpf(string cpf);
        Task<Cliente> Cadastrar(string nome, string email, string cpf);
    }
}