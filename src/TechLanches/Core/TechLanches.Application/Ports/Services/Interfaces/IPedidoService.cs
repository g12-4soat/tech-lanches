﻿using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Application.Ports.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<List<Pedido>> BuscarTodos();
        Task<Pedido> BuscarPorId(int idPedido);
        Task<List<Pedido>> BuscarPorStatus(StatusPedido statusPedido);
        Task<Pedido> Cadastrar(string? cpf, List<ItemPedido> itemPedidos);
        Task<Pedido> TrocarStatus(int pedidoId, StatusPedido statusPedido);
        Task Atualizar(int pedidoId, int clienteId, List<ItemPedido> itensPedido);
    }
}