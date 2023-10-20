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
    [Migration("20231016222618_pedido")]
    partial class pedido
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Pedido", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Valor");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos", (string)null);
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

            modelBuilder.Entity("TechLanches.Domain.ValueObjects.ItemPedido", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("PedidoId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecoProduto")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("PrecoProduto");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int")
                        .HasColumnName("Quantidade");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Valor");

                    b.HasKey("ProdutoId", "PedidoId");

                    b.HasIndex("PedidoId");

                    b.ToTable("ItemPedido", (string)null);
                });

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Pedido", b =>
                {
                    b.HasOne("TechLanches.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("TechLanches.Domain.ValueObjects.StatusPedido", "StatusPedido", b1 =>
                        {
                            b1.Property<int>("PedidoId")
                                .HasColumnType("int");

                            b1.Property<int>("Id")
                                .HasColumnType("int")
                                .HasColumnName("StatusPedidoId");

                            b1.HasKey("PedidoId");

                            b1.ToTable("Pedidos");

                            b1.WithOwner()
                                .HasForeignKey("PedidoId");
                        });

                    b.Navigation("Cliente");

                    b.Navigation("StatusPedido")
                        .IsRequired();
                });

            modelBuilder.Entity("TechLanches.Domain.ValueObjects.ItemPedido", b =>
                {
                    b.HasOne("TechLanches.Domain.Aggregates.Pedido", "Pedido")
                        .WithMany("ItensPedido")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("TechLanches.Domain.Aggregates.Pedido", b =>
                {
                    b.Navigation("ItensPedido");
                });

            modelBuilder.Entity("TechLanches.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Pedidos");
                });
#pragma warning restore 612, 618
        }
    }
}