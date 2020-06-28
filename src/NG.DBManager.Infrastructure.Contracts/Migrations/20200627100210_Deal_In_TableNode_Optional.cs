using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Deal_In_TableNode_Optional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Node_Deal_DealId",
                table: "Node");

            migrationBuilder.AlterColumn<Guid>(
                name: "DealId",
                table: "Node",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Deal_DealId",
                table: "Node",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Node_Deal_DealId",
                table: "Node");

            migrationBuilder.AlterColumn<Guid>(
                name: "DealId",
                table: "Node",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Deal_DealId",
                table: "Node",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
