using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechLanches.Adapter.SqlServer.Migrations
{
    public partial class formapagamento : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FormaPagamento",
                table: "Pagamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FormaPagamento",
                table: "Pagamentos");
        }
    }
}
