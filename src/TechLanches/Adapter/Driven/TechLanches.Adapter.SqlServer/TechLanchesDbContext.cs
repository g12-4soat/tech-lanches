using Microsoft.EntityFrameworkCore;
using TechLanches.Core;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.SqlServer
{
    public class TechLanchesDbContext : DbContext, IUnitOfWork
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }

        public TechLanchesDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TechLanchesDbContext).Assembly);
        }

        public async Task CommitAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}
