using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Enums;

namespace TechLanches.Adapter.SqlServer.EntityTypeConfigurations
{
    public class PagamentoEntityTypeConfiguration : IEntityTypeConfiguration<Pagamento>
    {
        public void Configure(EntityTypeBuilder<Pagamento> builder)
        {
            builder.ToTable("Pagamentos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Valor)
                  .HasColumnName("Valor")
                  .IsRequired();

            builder.Property(x => x.StatusPagamento)
                  .HasColumnName("StatusPagamento")
                  .HasConversion(
                    v => v.ToString(),
                    v => (StatusPagamento)Enum.Parse(typeof(StatusPagamento), v))
                  .IsRequired();

            // Revisar regra se essa relação deve ser 1 -> N
            builder.HasOne(x => x.Pedido)
                .WithMany(x => x.Pagamentos)
                .HasForeignKey(x => x.PedidoId);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}