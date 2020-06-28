using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class NoImage_Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Image",
                columns: new[] { "Id", "Name", "NodeId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "No Image", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Image",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));
        }
    }
}
