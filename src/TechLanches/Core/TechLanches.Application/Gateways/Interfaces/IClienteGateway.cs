using TechLanches.Domain.Entities;

namespace TechLanches.Application.Gateways.Interfaces
{
    public interface IClienteGateway:IRepositoryGateway
    {
        Task<Cliente> BuscarPorCpf(string cpf);
        Task<Cliente> Cadastrar(string nome, string email, string cpf);
        Task<Cliente> BuscarPorId(int idCliente);
    }
}