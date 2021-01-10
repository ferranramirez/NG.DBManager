using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Add3Fields_DealTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessMessage",
                table: "Deal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Conditions",
                table: "Deal",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserMessage",
                table: "Deal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessMessage",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "Conditions",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "UserMessage",
                table: "Deal");
        }
    }
}
