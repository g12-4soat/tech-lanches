using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Application.DTOs;

namespace TechLanches.Adapter.SqlServer.EntityTypeConfigurations
{
    public class FilaPedidoEntityTypeConfiguration : IEntityTypeConfiguration<FilaPedido>
    {
        public void Configure(EntityTypeBuilder<FilaPedido> builder)
        {
            builder.HasNoKey();
        }
    }
}