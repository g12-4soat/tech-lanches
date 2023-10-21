using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.SqlServer.EntityTypeConfigurations
{
    public class PedidoEntityTypeConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Valor)
                   .HasColumnName("Valor")
                   .IsRequired();

            builder.Property(x => x.StatusPedido)
                  .HasColumnName("StatusPedido")
                  .HasConversion(
                    v => v.ToString(),
                    v => (StatusPedido)Enum.Parse(typeof(StatusPedido), v))
                  .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(x => x.ClienteId)
                .IsRequired(false);

            builder.Ignore(x => x.DomainEvents);

            var navigation = builder.Metadata.FindNavigation(nameof(Pedido.ItensPedido));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}