using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Adapter.SqlServer.EntityTypeConfigurations
{
    public class ItemPedidoEntityTypeConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItemPedido");

            builder.HasKey(x => new { x.ProdutoId, x.PedidoId });

            builder.HasIndex(x => new { x.ProdutoId, x.PedidoId }).IsUnique();

            builder.Property(x => x.Quantidade)
                   .HasColumnName("Quantidade")
                   .IsRequired();

            builder.Property(x => x.PrecoProduto)
                   .HasColumnName("PrecoProduto")
                   .IsRequired();

            builder.Property(x => x.Valor)
                   .HasColumnName("Valor")
                   .IsRequired();

            builder.HasOne(x => x.Pedido)
                .WithMany(p => p.ItensPedido)
                .HasForeignKey(x => x.PedidoId);

            builder.HasOne(x => x.Produto)
                .WithMany(p => p.ItensPedidos)
                .HasForeignKey(x => x.ProdutoId);
        }
    }
}