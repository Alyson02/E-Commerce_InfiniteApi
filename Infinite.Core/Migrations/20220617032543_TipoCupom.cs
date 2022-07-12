using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class TipoCupom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //Bugou, tive que tirar tudo
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Compra_Cupom_CupomId",
                table: "Compra");

            migrationBuilder.DropForeignKey(
                name: "FK_Cupom_TipoCupom_TipoCupomId",
                table: "Cupom");

            migrationBuilder.DropForeignKey(
                name: "FK_Funcionario_Cupom_CupomId",
                table: "Funcionario");

            migrationBuilder.DropTable(
                name: "TipoCupom");

            migrationBuilder.DropIndex(
                name: "IX_Cupom_TipoCupomId",
                table: "Cupom");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Cupom");

            migrationBuilder.DropColumn(
                name: "TipoCupomId",
                table: "Cupom");

            migrationBuilder.RenameColumn(
                name: "VendasRealizadas",
                table: "Cupom",
                newName: "Quantidade");

            migrationBuilder.AlterColumn<int>(
                name: "CupomId",
                table: "Funcionario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CupomId",
                table: "Cupom",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Cupom",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "CupomId",
                table: "Compra",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Compra_Cupom_CupomId",
                table: "Compra",
                column: "CupomId",
                principalTable: "Cupom",
                principalColumn: "CupomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Funcionario_Cupom_CupomId",
                table: "Funcionario",
                column: "CupomId",
                principalTable: "Cupom",
                principalColumn: "CupomId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
