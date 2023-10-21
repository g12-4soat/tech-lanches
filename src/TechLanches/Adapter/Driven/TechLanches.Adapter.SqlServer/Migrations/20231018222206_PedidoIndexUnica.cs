using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLanches.Adapter.SqlServer.Migrations
{
    public partial class PedidoIndexUnica : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ItemPedido_ProdutoId_PedidoId",
                table: "ItemPedido",
                columns: new[] { "ProdutoId", "PedidoId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ItemPedido_ProdutoId_PedidoId",
                table: "ItemPedido");
        }
    }
}
