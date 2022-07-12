using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class CarrinhoFixNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarrinhoID",
                table: "Carrinho",
                newName: "CarrinhoId");

            migrationBuilder.RenameColumn(
                name: "StatusCar",
                table: "Carrinho",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCadastro",
                table: "Carrinho",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFechamento",
                table: "Carrinho",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCadastro",
                table: "Carrinho");

            migrationBuilder.DropColumn(
                name: "DataFechamento",
                table: "Carrinho");

            migrationBuilder.RenameColumn(
                name: "CarrinhoId",
                table: "Carrinho",
                newName: "CarrinhoID");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Carrinho",
                newName: "StatusCar");
        }
    }
}
