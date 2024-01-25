namespace TechLanches.Domain.Enums;
public enum StatusPedido
{
    PedidoCriado = 1,
    PedidoRecebido,
    PedidoCancelado,
    PedidoCanceladoPorPagamentoRecusado,
    PedidoEmPreparacao,
    PedidoPronto,
    PedidoFinalizado,
    PedidoRetirado,
    PedidoDescartado
}
