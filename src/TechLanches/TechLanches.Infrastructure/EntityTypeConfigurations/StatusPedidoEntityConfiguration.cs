using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure.EntityTypeConfigurations
{
    //public class StatusPedidoEntityTypeConfiguration : IEntityTypeConfiguration<StatusPedido>
    //{
    //    public void Configure(EntityTypeBuilder<StatusPedido> builder)
    //    {
    //        builder.ToTable("StatusPedido");

    //        builder.HasKey(x => x.Id);
    //        builder.Property(x => x.Id).ValueGeneratedOnAdd();

    //        builder.Property(x => x.Nome)
    //               .HasColumnName("Nome")
    //               .IsRequired();
    //    }
    //}
}