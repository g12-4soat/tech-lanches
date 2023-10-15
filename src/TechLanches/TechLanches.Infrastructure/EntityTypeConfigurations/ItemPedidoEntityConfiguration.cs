using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure.EntityTypeConfigurations
{
    //public class ItemPedidoEntityTypeConfiguration : IEntityTypeConfiguration<ItemPedido>
    //{
    //    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    //    {
    //        builder.ToTable("ItemPedido");

    //        builder.HasKey(x => x.Id);
    //        builder.Property(x => x.Id).ValueGeneratedOnAdd();

    //        builder.Property(x => x.ProdutoId)
    //               .HasColumnName("ProdutoId")
    //               .IsRequired();

    //        builder.Property(x => x.PedidoId)
    //               .HasColumnName("PedidoId")
    //               .IsRequired();

    //        builder.Property(x => x.Quantidade)
    //               .HasColumnName("Quantidade")
    //               .IsRequired();

    //        builder.Property(x => x.PrecoProduto)
    //               .HasColumnName("PrecoProduto")
    //               .IsRequired();

    //        builder.Property(x => x.Valor)
    //               .HasColumnName("Valor")
    //               .IsRequired();

    //        builder.Ignore(x => x.DomainEvents);
    //    }
    //}
}