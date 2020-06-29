using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class UpdateLocation_FK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commerce_Location_LocationId",
                table: "Commerce");

            migrationBuilder.DropIndex(
                name: "IX_Commerce_LocationId",
                table: "Commerce");

            migrationBuilder.AddColumn<Guid>(
                name: "CommerceId",
                table: "Location",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Location_CommerceId",
                table: "Location",
                column: "CommerceId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Location_Commerce_CommerceId",
                table: "Location",
                column: "CommerceId",
                principalTable: "Commerce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Location_Commerce_CommerceId",
                table: "Location");

            migrationBuilder.DropIndex(
                name: "IX_Location_CommerceId",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "CommerceId",
                table: "Location");

            migrationBuilder.CreateIndex(
                name: "IX_Commerce_LocationId",
                table: "Commerce",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commerce_Location_LocationId",
                table: "Commerce",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
