using TechLanches.Application.Controllers.Interfaces;
using TechLanches.Application.DTOs;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Presenters.Interfaces;
using TechLanches.Application.UseCases.Clientes;

namespace TechLanches.Application.Controllers
{
    public class ClienteController : IClienteController
    {
        private readonly IClientePresenter _clientePresenter;
        private readonly IClienteGateway _clienteGateway;

        public ClienteController(IClientePresenter clientePresenter, IClienteGateway clienteGateway)
        {
            _clientePresenter = clientePresenter;
            _clienteGateway = clienteGateway;
        }

        public async Task<ClienteResponseDTO> BuscarPorCpf(string cpf)
        {
            var cliente = await ClienteUseCases.IdentificarCliente(cpf,_clienteGateway);
            return _clientePresenter.ParaDto(cliente);
        }

        public async Task<ClienteResponseDTO> BuscarPorId(int idCliente)
        {
            var cliente = await _clienteGateway.BuscarPorId(idCliente);
            return _clientePresenter.ParaDto(cliente);
        }

        public async Task<ClienteResponseDTO> Cadastrar(string nome, string email, string cpf)
        {
            var novoCliente = await ClienteUseCases.Cadastrar(nome, email, cpf, _clienteGateway);
            await _clienteGateway.CommitAsync();
            return _clientePresenter.ParaDto(novoCliente);
        }
    }
}