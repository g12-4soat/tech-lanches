using TechLanches.Core;

namespace TechLanches.Domain.ValueObjects
{
    public class StatusPedido : Enumeration
    {
        public static StatusPedido PedidoCriado = new(1, nameof(PedidoCriado));
        public static StatusPedido PedidoEmPreparacao = new(2, nameof(PedidoEmPreparacao));
        public static StatusPedido PedidoPronto = new(3, nameof(PedidoPronto));
        public static StatusPedido PedidoRetirado = new(4, nameof(PedidoRetirado));
        public static StatusPedido PedidoDescartado = new(5, nameof(PedidoDescartado));
        public static StatusPedido PedidoCancelado = new(6, nameof(PedidoCancelado));
        public static StatusPedido PedidoFinalizado = new(7, nameof(PedidoFinalizado));

        public StatusPedido(int id, string nome)
            : base(id, nome)
        {
        }

        private StatusPedido() { }

        public static IEnumerable<StatusPedido> List() =>
                new[] 
                { 
                    PedidoCriado, 
                    PedidoEmPreparacao, 
                    PedidoPronto,
                    PedidoRetirado, 
                    PedidoDescartado,
                    PedidoCancelado,
                    PedidoFinalizado
                };

        public static StatusPedido From(int id)
        {
            var status = List().SingleOrDefault(s => s.Id == id);
            return status is null
                ? throw new DomainException($"Possiveis valores para status: {string.Join(",", List().Select(s => $"{s.Id}-{s.Nome}"))}")
                : status;
        }
    }
}