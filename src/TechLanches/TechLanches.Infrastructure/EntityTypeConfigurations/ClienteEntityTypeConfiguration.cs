using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Entities;
using TechLanches.Domain.ValueObjects;

namespace TechLanches.Infrastructure.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.CPF)
                .HasColumnName("Cpf")
                .HasConversion(
                    x => x.Numero,
                    y => new Cpf(y))
                .IsRequired();

            builder.Property(x => x.Nome)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnName("Email")
                .HasConversion(
                    x => x.EnderecoEmail,
                    y => new Email(y))
                .IsRequired();

            builder.HasIndex(x => x.CPF);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}