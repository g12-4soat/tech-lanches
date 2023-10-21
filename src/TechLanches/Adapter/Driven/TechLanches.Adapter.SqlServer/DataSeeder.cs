using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;

namespace TechLanches.Adapter.SqlServer
{
    public static class DataSeeder
    {
        public static void Seed(TechLanchesDbContext context)
        {
            ClientesSeed(context);
            ProdutosSeed(context);
        }

        private static void ClientesSeed(TechLanchesDbContext context)
        {
            if (!context.Clientes.Any())
            {
                var countries = new List<Cliente>
                {
                    new Cliente("João Silva", "joao.silva@example.com", "02562905040"),
                    new Cliente("Maria Santos", "maria.santos@example.com", "12687590070"),
                    new Cliente("José Pereira", "jose.pereira@example.com", "18696905083")
                };

                context.AddRange(countries);
                context.SaveChanges();
            }
        }

        private static void ProdutosSeed(TechLanchesDbContext context)
        {
            if (!context.Produtos.Any())
            {
                var produtos = new List<Produto>
                {
                    new Produto("X-Burguer", "Lanche com pão carne e queijo", 30, 1),
                    new Produto("Batata Frita", "Fritas comum", 8, 2),
                    new Produto("Suco de Laranja", "Suco natural de laranjas", 10, 3),
                    new Produto("Sorvete", "Casquinha sabor chocolate ou baunilha", 3, 4),
                };

                context.AddRange(produtos);
                context.SaveChanges();
            }
        }
    }
}
