using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLanches.Infrastructure.Migrations
{
    public partial class StatusPedidoEmString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "StatusPedido",
                table: "Pedidos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
