using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.Enums;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.SqlServer
{
    public static class DataSeeder
    {
        public static void Seed(TechLanchesDbContext context)
        {
            ClientesSeed(context);
            ProdutosSeed(context);
            PedidosSeed(context);
            PagamentosSeed(context);
        }

        private static void ClientesSeed(TechLanchesDbContext context)
        {
            if (!context.Clientes.Any())
            {
                var clientes = new List<Cliente>
                {
                    new ("João Silva", "joao.silva@example.com", "02562905040"),
                    new ("Maria Santos", "maria.santos@example.com", "12687590070"),
                    new ("José Pereira", "jose.pereira@example.com", "18696905083")
                };

                context.AddRange(clientes);
                context.SaveChanges();
            }
        }

        private static void ProdutosSeed(TechLanchesDbContext context)
        {
            if (!context.Produtos.Any())
            {
                var produtos = new List<Produto>
                {
                    new ("X-Burguer", "Lanche com pão carne e queijo", 30, 1),
                    new ("Batata Frita", "Fritas comum", 8, 2),
                    new ("Suco de Laranja", "Suco natural de laranjas", 10, 3),
                    new ("Sorvete", "Casquinha sabor chocolate ou baunilha", 3, 4),
                };

                context.AddRange(produtos);
                context.SaveChanges();
            }
        }

        private static void PedidosSeed(TechLanchesDbContext context)
        {
            if (!context.Pedidos.Any())
            {
                var produto1 = context.Produtos.Find(1);
                var produto2 = context.Produtos.Find(2);
                var pedidos = new List<Pedido>
                {
                    new (1, new List<ItemPedido> { new (produto1!.Id, 1, produto1.Preco), new (produto2!.Id, 2, produto2.Preco) })
                };

                context.AddRange(pedidos);
                context.SaveChanges();
            }
        }

        private static void PagamentosSeed(TechLanchesDbContext context)
        {
            if (!context.Pagamentos.Any())
            {
                const int PEDIDO_ID = 1;
                var pedido = context.Pedidos.Find(PEDIDO_ID);

                if (pedido is null) throw new NullReferenceException(nameof(pedido));

                var pagamento = new Pagamento(pedido.Id, pedido.Valor, FormaPagamento.QrCodeMercadoPago);

                context.Add(pagamento);
                context.SaveChanges();
            }
        }
    }
}