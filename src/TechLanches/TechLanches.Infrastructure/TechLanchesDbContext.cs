using Microsoft.EntityFrameworkCore;
using System;
using TechLanches.Core;
using TechLanches.Domain.Entities;

namespace TechLanches.Infrastructure
{
    public class TechLanchesDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Cliente> Clientes { get; set; }

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
