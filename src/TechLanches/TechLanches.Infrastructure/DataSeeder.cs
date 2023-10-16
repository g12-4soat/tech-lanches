using TechLanches.Domain.Entities;

namespace TechLanches.Infrastructure
{
    public static class DataSeeder
    {
        public static void Seed(TechLanchesDbContext context)
        {
            ClientesSeed(context);
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
    }
}
