using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class AddCarrinhoToCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarrinhoId",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Compra_CarrinhoId",
                table: "Compra",
                column: "CarrinhoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Carrinho_CarrinhoId",
                table: "Compra",
                column: "CarrinhoId",
                principalTable: "Carrinho",
                principalColumn: "CarrinhoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Carrinho_CarrinhoId",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_CarrinhoId",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "CarrinhoId",
                table: "Compra");
        }
    }
}
