using TechLanches.Application.Ports.Repositories;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly IPedidoService _pedidoService;
        private readonly IClienteService _clienteService;
        private readonly IProdutoService _produtoService;

        public CheckoutService(IPedidoService pedidoService, 
                               IProdutoService produtoService,
                               IClienteService clienteService)
        {
            _pedidoService = pedidoService;
            _clienteService = clienteService;
            _produtoService = produtoService;   
        }

        public async Task<List<string>> ValidaPedido(int pedidoId)
        {
            //pedido existe?
            var pedido = await _pedidoService.BuscarPorId(pedidoId);

            if(pedido is null) return new List<string>() { $"Pedido não encontrado para checkout - IdPedido: {pedidoId}" };

            //cliente existe?
            //var clienteIdentificado = pedido.ClienteIdentificado;

            var resultadoValidacao = new List<string>();

            //produto existe?
            foreach (var item in pedido.ItensPedido)
            {
                var itemExistente = await _produtoService.BuscarPorId(item.ProdutoId);

                if (itemExistente is null)
                    resultadoValidacao.Add($"Produto não disponível - IdProduto: {item.ProdutoId}");

                //qtd de produto disponível em estoque?
            }

            //qual statusPedido é esperado para fazer o checkout
            if (pedido.StatusPedido != StatusPedido.PedidoCriado)
                resultadoValidacao.Add($"Status não autorizado para checkout - Status: {pedido.StatusPedido}");

            return resultadoValidacao;
        }
    }
}
