using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Add_DealType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DealTypeId",
                table: "Deal",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "DealType",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Deal_DealTypeId",
                table: "Deal",
                column: "DealTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Deal_DealType_DealTypeId",
                table: "Deal",
                column: "DealTypeId",
                principalTable: "DealType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Deal_DealType_DealTypeId",
                table: "Deal");

            migrationBuilder.DropTable(
                name: "DealType");

            migrationBuilder.DropIndex(
                name: "IX_Deal_DealTypeId",
                table: "Deal");

            migrationBuilder.DropColumn(
                name: "DealTypeId",
                table: "Deal");
        }
    }
}
