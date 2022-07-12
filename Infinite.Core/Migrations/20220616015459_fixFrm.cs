using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class fixFrm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_TipoUsuario_TipoUsuarioId1",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_TipoUsuarioId1",
                table: "Usuario");

            migrationBuilder.DropColumn(
                name: "TipoUsuarioId1",
                table: "Usuario");

            migrationBuilder.AlterColumn<int>(
                name: "TipoUsuarioId",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Frm",
                table: "FormaPag",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_TipoUsuario_TipoUsuarioId",
                table: "Usuario",
                column: "TipoUsuarioId",
                principalTable: "TipoUsuario",
                principalColumn: "TipoUsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_TipoUsuario_TipoUsuarioId",
                table: "Usuario");

            migrationBuilder.DropIndex(
                name: "IX_Usuario_TipoUsuarioId",
                table: "Usuario");

            migrationBuilder.AlterColumn<string>(
                name: "TipoUsuarioId",
                table: "Usuario",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "TipoUsuarioId1",
                table: "Usuario",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Frm",
                table: "FormaPag",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_TipoUsuarioId1",
                table: "Usuario",
                column: "TipoUsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_TipoUsuario_TipoUsuarioId1",
                table: "Usuario",
                column: "TipoUsuarioId1",
                principalTable: "TipoUsuario",
                principalColumn: "TipoUsuarioId");
        }
    }
}
