using TechLanches.Adapter.RabbitMq.Messaging;
using TechLanches.Application.Gateways.Interfaces;
using TechLanches.Application.Ports.Services.Interfaces;
using TechLanches.Application.UseCases.Clientes;
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
            var cliente = await ClienteUseCases.IdentificarCliente(cpf, clienteGateway);
            var pedido = new Pedido(cliente?.Id, itensPedido);

            pedido = await pedidoGateway.Cadastrar(pedido);
            return pedido;
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
