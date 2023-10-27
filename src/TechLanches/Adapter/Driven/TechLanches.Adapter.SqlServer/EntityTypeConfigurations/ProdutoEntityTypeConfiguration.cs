using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Aggregates;

namespace TechLanches.Adapter.SqlServer.EntityTypeConfigurations
{
    public class ProdutoEntityTypeConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Nome)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasIndex(x => x.Nome);

            builder.Property(x => x.Descricao)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(x => x.Preco)
                .IsRequired();

            builder.OwnsOne(x => x.Categoria,
                navigationBuilder =>
                {
                    navigationBuilder
                        .Property(categoria => categoria.Id)
                        .HasColumnName("CategoriaId")
                        .IsRequired();

                    navigationBuilder.HasIndex(categoria => categoria.Id);

                    navigationBuilder.Ignore(categoria => categoria.Nome);
                });

            builder.Ignore(x => x.DomainEvents);

            builder.HasQueryFilter(b => !b.Deletado);
        }
    }
}