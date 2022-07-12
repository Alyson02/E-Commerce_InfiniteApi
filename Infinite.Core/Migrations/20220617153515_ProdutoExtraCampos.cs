using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class ProdutoExtraCampos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "CapaId",
            //    table: "Produto",
            //    type: "int",
            //    nullable: false,
            //    defaultValue: 0);

            //migrationBuilder.AddColumn<string>(
            //    name: "Descricao",
            //    table: "Produto",
            //    type: "longtext",
            //    nullable: true)
            //    .Annotation("MySql:CharSet", "utf8mb4");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Produto_CapaId",
            //    table: "Produto",
            //    column: "CapaId");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Produto_Arquivo_CapaId",
            //    table: "Produto",
            //    column: "CapaId",
            //    principalTable: "Arquivo",
            //    principalColumn: "ArquivoId",
            //    onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Arquivo_CapaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CapaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CapaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Descricao",
                table: "Produto");
        }
    }
}
