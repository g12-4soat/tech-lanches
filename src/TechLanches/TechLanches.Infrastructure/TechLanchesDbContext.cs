using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;

namespace TechLanches.Infrastructure
{
    public class TechLanchesDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }

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

        public static class DataSeeder
        {
            public static void Seed(TechLanchesDbContext context)
            {
                ProdutosSeed(context);
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
}
