using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Infrastructure.EntityTypeConfigurations
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

            builder.OwnsOne(x => x.StatusPedido,
                  navigationBuilder =>
                  {
                      navigationBuilder
                          .Property(s => s.Id)
                          .HasColumnName("StatusPedido")
                          .IsRequired();

                      navigationBuilder.Ignore(s => s.Nome);
                  });

            builder.HasOne(x => x.Cliente)
                .WithMany(c => c.Pedidos)
                .HasForeignKey(x => x.ClienteId);

            builder.Ignore(x => x.DomainEvents);

            var navigation = builder.Metadata.FindNavigation(nameof(Pedido.ItensPedido));
            navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}