using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Infrastructure.EntityTypeConfigurations
{
    public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Descricao)
                .IsRequired();

            builder.Property(x => x.Preco)
                .IsRequired();

            builder.OwnsOne(x => x.Categoria,
                y => {
                    y.Property(z => z.Id)
                        .HasColumnName("Categoria_Id");
                });

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
