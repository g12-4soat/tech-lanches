using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechLanches.Domain.Entities;

namespace TechLanches.Adapter.SqlServer.EntityTypeConfigurations
{
    public class ClienteEntityTypeConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.OwnsOne(x => x.CPF,
                navigationBuilder =>
                {
                    navigationBuilder
                        .Property(cpf => cpf.Numero)
                        .HasColumnName("Cpf")
                        .HasMaxLength(11)
                        .IsRequired();

                    navigationBuilder
                        .HasIndex(cpf => cpf.Numero)
                        .IsUnique();
                });

            builder.OwnsOne(x => x.Email,
                navigationBuilder =>
                {
                    navigationBuilder
                        .Property(email => email.EnderecoEmail)
                        .HasColumnName("Email")
                        .HasMaxLength(100)
                        .IsRequired();

                    navigationBuilder
                        .HasIndex(email => email.EnderecoEmail);
                });

            builder.Property(x => x.Nome)
                .HasMaxLength(50)
                .IsRequired();

            builder.Ignore(x => x.DomainEvents);
        }
    }
}