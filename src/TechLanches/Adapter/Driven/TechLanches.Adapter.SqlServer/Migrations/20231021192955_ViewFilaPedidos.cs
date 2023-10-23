using Microsoft.EntityFrameworkCore.Migrations;
using TechLanches.Domain.Enums;

#nullable disable

namespace TechLanches.Adapter.SqlServer.Migrations
{
    public partial class ViewFilaPedidos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
               $@"CREATE OR ALTER VIEW FilaPedidos AS
                    SELECT Id AS PedidoId FROM Pedidos
                    WHERE StatusPedido in 
                    ('{StatusPedido.PedidoCriado}', 
                    '{StatusPedido.PedidoEmPreparacao}');");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW FilaPedidos;");
        }
    }
}
