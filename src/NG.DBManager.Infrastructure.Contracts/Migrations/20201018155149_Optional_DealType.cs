using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Optional_DealType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_DealType_DealTypeId",
                table: "Deal");

            migrationBuilder.AlterColumn<Guid>(
                name: "DealTypeId",
                table: "Deal",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_DealType_DealTypeId",
                table: "Deal",
                column: "DealTypeId",
                principalTable: "DealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_DealType_DealTypeId",
                table: "Deal");

            migrationBuilder.AlterColumn<Guid>(
                name: "DealTypeId",
                table: "Deal",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_DealType_DealTypeId",
                table: "Deal",
                column: "DealTypeId",
                principalTable: "DealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
