using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class FixCompra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Cartao_CartaoCardId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Endereco_EnderecoEndId",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_CartaoCardId",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_EnderecoEndId",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "CartaoCardId",
                table: "Compra");

            migrationBuilder.DropColumn(
                name: "EnderecoEndId",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "FrmID",
                table: "Pagamento",
                newName: "FormaId");

            migrationBuilder.RenameColumn(
                name: "EndId",
                table: "Compra",
                newName: "EnderecoId");

            migrationBuilder.RenameColumn(
                name: "CardId",
                table: "Compra",
                newName: "CartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_CartaoId",
                table: "Compra",
                column: "CartaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_EnderecoId",
                table: "Compra",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Cartao_CartaoId",
                table: "Compra",
                column: "CartaoId",
                principalTable: "Cartao",
                principalColumn: "CardId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Endereco_EnderecoId",
                table: "Compra",
                column: "EnderecoId",
                principalTable: "Endereco",
                principalColumn: "EndId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Cartao_CartaoId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Endereco_EnderecoId",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_CartaoId",
                table: "Compra");

            migrationBuilder.DropIndex(
                name: "IX_Compra_EnderecoId",
                table: "Compra");

            migrationBuilder.RenameColumn(
                name: "FormaId",
                table: "Pagamento",
                newName: "FrmID");

            migrationBuilder.RenameColumn(
                name: "EnderecoId",
                table: "Compra",
                newName: "EndId");

            migrationBuilder.RenameColumn(
                name: "CartaoId",
                table: "Compra",
                newName: "CardId");

            migrationBuilder.AddColumn<int>(
                name: "CartaoCardId",
                table: "Compra",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EnderecoEndId",
                table: "Compra",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Compra_CartaoCardId",
                table: "Compra",
                column: "CartaoCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Compra_EnderecoEndId",
                table: "Compra",
                column: "EnderecoEndId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Cartao_CartaoCardId",
                table: "Compra",
                column: "CartaoCardId",
                principalTable: "Cartao",
                principalColumn: "CardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Endereco_EnderecoEndId",
                table: "Compra",
                column: "EnderecoEndId",
                principalTable: "Endereco",
                principalColumn: "EndId");
        }
    }
}
