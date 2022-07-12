using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class FixCompraCard : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Cartao_CartaoId",
                table: "Compra");

            migrationBuilder.AlterColumn<int>(
                name: "CartaoId",
                table: "Compra",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Cartao_CartaoId",
                table: "Compra",
                column: "CartaoId",
                principalTable: "Cartao",
                principalColumn: "CardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Cartao_CartaoId",
                table: "Compra");

            migrationBuilder.AlterColumn<int>(
                name: "CartaoId",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Cartao_CartaoId",
                table: "Compra",
                column: "CartaoId",
                principalTable: "Cartao",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
