using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.Services;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.UseCases.Pedidos
{
    public class PedidoUseCases
    {
        public static async Task<Pedido> Cadastrar(string? cpf, List<ItemPedido> itensPedido, IPedidoGateway pedidoGateway, IClienteGateway clienteGateway)
        {
            var cliente = await IdentificarCliente(cpf, clienteGateway);
            var pedido = new Pedido(cliente?.Id, itensPedido);

            pedido = await pedidoGateway.Cadastrar(pedido);
            return pedido;
        }

        private static async Task<Cliente?> IdentificarCliente(string? cpf, IClienteGateway clienteGateway)
        {
            if (cpf is null) return null;

            var clienteExistente = await clienteGateway.BuscarPorCpf(new Cpf(cpf));

            if (clienteExistente is null) throw new DomainException("Cliente não cadastrado!");

            return clienteExistente;
        }

        public static async Task<Pedido> TrocarStatus(
            int pedidoId, 
            StatusPedido statusPedido, 
            IPedidoGateway pedidoGateway, 
            IStatusPedidoValidacaoService statusPedidoValidacaoService, 
            IRabbitMqService rabbitMqService)
        {
            var pedido = await pedidoGateway.BuscarPorId(pedidoId)
               ?? throw new DomainException("Não foi encontrado nenhum pedido com id informado.");

            pedido.TrocarStatus(statusPedidoValidacaoService, statusPedido);

            if (statusPedido == StatusPedido.PedidoRecebido)
                rabbitMqService.Publicar(pedidoId);

            return pedido;
        }
    }
}
