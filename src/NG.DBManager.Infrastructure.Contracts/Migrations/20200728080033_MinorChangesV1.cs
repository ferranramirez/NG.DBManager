using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class MinorChangesV1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Surname",
                table: "User");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Tour",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"),
                column: "Name",
                value: "Admin QA User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("440edb6b-342e-4d5f-a233-62aef964cbfa"),
                column: "Name",
                value: "Commerce QA User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                column: "Name",
                value: "FullCommerce QA User");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b0f2451e-5820-4eca-a797-46a01693a3b2"),
                column: "Name",
                value: "Basic QA User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Tour");

            migrationBuilder.AddColumn<string>(
                name: "Surname",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"),
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Admin", "QA User" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("440edb6b-342e-4d5f-a233-62aef964cbfa"),
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Commerce", "QA User" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                columns: new[] { "Name", "Surname" },
                values: new object[] { "FullCommerce", "QA User" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b0f2451e-5820-4eca-a797-46a01693a3b2"),
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Basic", "QA User" });
        }
    }
}
