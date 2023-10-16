using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure
{
    public class TechLanchesDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }

        public TechLanchesDbContext(DbContextOptions options) : base (options)
        {
            //mediatr para publicar eventos
        }    

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechLanchesDbContext).Assembly);
        }

        public async Task Commit()
        {
            await base.SaveChangesAsync();
            
            //publicar eventos
        }
    }

    public static class DataSeeder
    {
        public static void Seed(TechLanchesDbContext context)
        {
            ClientesSeed(context);
            PedidosSeed(context);
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

        private static void PedidosSeed(TechLanchesDbContext context)
        {
            if (!context.Pedidos.Any())
            {
                var countries = new List<Pedido>
                {
                    new Pedido(1, new List<ItemPedido> { new ItemPedido(1, 1, 1, 1), new ItemPedido(2, 2, 2, 2) })
                };

                context.AddRange(countries);
                context.SaveChanges();
            }
        }
    }
}
