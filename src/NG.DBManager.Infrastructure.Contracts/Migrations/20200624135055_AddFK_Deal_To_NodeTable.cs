using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class AddFK_Deal_To_NodeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DealId",
                table: "Node",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Node_DealId",
                table: "Node",
                column: "DealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Deal_DealId",
                table: "Node",
                column: "DealId",
                principalTable: "Deal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Node_Deal_DealId",
                table: "Node");

            migrationBuilder.DropIndex(
                name: "IX_Node_DealId",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "DealId",
                table: "Node");
        }
    }
}
