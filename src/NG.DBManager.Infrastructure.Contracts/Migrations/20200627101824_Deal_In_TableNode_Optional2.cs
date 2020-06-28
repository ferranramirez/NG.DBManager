using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Deal_In_TableNode_Optional2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Node_Location_LocationId",
                table: "Node");

            migrationBuilder.DropColumn(
                name: "CoordinatesId",
                table: "Node");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Node",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Location_LocationId",
                table: "Node",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Node_Location_LocationId",
                table: "Node");

            migrationBuilder.AlterColumn<Guid>(
                name: "LocationId",
                table: "Node",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "CoordinatesId",
                table: "Node",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Location_LocationId",
                table: "Node",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
