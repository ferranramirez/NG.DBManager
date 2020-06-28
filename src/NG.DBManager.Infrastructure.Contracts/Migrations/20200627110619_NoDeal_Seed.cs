using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class NoDeal_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Deal",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "No Deal" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Deal",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
