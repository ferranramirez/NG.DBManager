using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class UserWithManyCommerces_CommerceIsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Commerce_UserId",
                table: "Commerce");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Commerce",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Commerce_UserId",
                table: "Commerce",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Commerce_UserId",
                table: "Commerce");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Commerce");

            migrationBuilder.CreateIndex(
                name: "IX_Commerce_UserId",
                table: "Commerce",
                column: "UserId",
                unique: true);
        }
    }
}
