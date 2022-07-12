using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infinite.Core.Migrations
{
    public partial class AddColumnStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Agendamento",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Agendamento");
        }
    }
}
