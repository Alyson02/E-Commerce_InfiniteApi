using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class CampoRuaEndereco : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_CupomCategoriaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CupomCategoriaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CupomCategoriaId",
                table: "Produto");

            migrationBuilder.AddColumn<string>(
                name: "NomeRua",
                table: "Endereco",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_CategoriaId",
                table: "Produto",
                column: "CategoriaId",
                principalTable: "Categoria",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_CategoriaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "NomeRua",
                table: "Endereco");

            migrationBuilder.AddColumn<int>(
                name: "CupomCategoriaId",
                table: "Produto",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CupomCategoriaId",
                table: "Produto",
                column: "CupomCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_CupomCategoriaId",
                table: "Produto",
                column: "CupomCategoriaId",
                principalTable: "Categoria",
                principalColumn: "CategoriaId");
        }
    }
}
