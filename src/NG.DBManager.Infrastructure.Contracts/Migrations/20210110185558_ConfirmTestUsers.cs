using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class ConfirmTestUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"),
                column: "EmailConfirmed",
                value: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("440edb6b-342e-4d5f-a233-62aef964cbfa"),
                column: "EmailConfirmed",
                value: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                column: "EmailConfirmed",
                value: true);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b0f2451e-5820-4eca-a797-46a01693a3b2"),
                column: "EmailConfirmed",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("0ac2c4c5-ebff-445e-85d4-1db76d65ce0a"),
                column: "EmailConfirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("440edb6b-342e-4d5f-a233-62aef964cbfa"),
                column: "EmailConfirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("73b7b257-41f7-4b22-9a10-93fb91238fd9"),
                column: "EmailConfirmed",
                value: false);

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b0f2451e-5820-4eca-a797-46a01693a3b2"),
                column: "EmailConfirmed",
                value: false);
        }
    }
}
