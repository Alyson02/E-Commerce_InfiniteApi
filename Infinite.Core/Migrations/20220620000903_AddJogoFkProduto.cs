using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class AddJogoFkProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "ProdutoId",
            //    table: "Jogo",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Jogo_ProdutoId",
            //    table: "Jogo",
            //    column: "ProdutoId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Jogo_Produto_ProdutoId",
            //    table: "Jogo",
            //    column: "ProdutoId",
            //    principalTable: "Produto",
            //    principalColumn: "ProdutoID",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogo_Produto_ProdutoId",
                table: "Jogo");

            migrationBuilder.DropIndex(
                name: "IX_Jogo_ProdutoId",
                table: "Jogo");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Jogo");
        }
    }
}
