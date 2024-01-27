using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Gateways.Interfaces
{
    public interface IClienteGateway : IRepositoryGateway
    {
        Task<Cliente> BuscarPorCpf(Cpf cpf);
        Task<Cliente> Cadastrar(Cliente cliente);
        Task<Cliente> BuscarPorId(int idCliente);
    }
}