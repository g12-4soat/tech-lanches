﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TechLanches.Infrastructure;

#nullable disable

namespace TechLanches.Infrastructure.Migrations
{
    [DbContext(typeof(TechLanchesDbContext))]
    [Migration("20231013174502_produto")]
    partial class produto
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Preco")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Produtos", (string)null);
                });

            modelBuilder.Entity("TechLanches.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Cpf");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Email");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CPF");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Produto", b =>
                {
                    b.OwnsOne("TechLanches.Domain.ValueObjects.CategoriaProduto", "Categoria", b1 =>
                        {
                            b1.Property<int>("ProdutoId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .HasColumnType("int")
                                .HasColumnName("Categoria_Id");

                            b1.HasKey("ProdutoId");

                            b1.ToTable("Produtos");

                            b1.WithOwner()
                                .HasForeignKey("ProdutoId");
                        });

                    b.Navigation("Categoria")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
