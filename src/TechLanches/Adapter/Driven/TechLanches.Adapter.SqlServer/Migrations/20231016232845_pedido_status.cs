using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLanches.Adapter.SqlServer.Migrations
{
    public partial class pedido_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusPedidoId",
                table: "Pedidos",
                newName: "StatusPedido");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusPedido",
                table: "Pedidos",
                newName: "StatusPedidoId");
        }
    }
}
