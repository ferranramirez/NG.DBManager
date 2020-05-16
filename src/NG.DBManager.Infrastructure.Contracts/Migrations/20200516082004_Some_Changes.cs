using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class Some_Changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Image_ImageId",
                table: "Tour");

            migrationBuilder.DropTable(
                name: "Featured");

            migrationBuilder.DropIndex(
                name: "IX_Tour_ImageId",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "Redemption",
                table: "Coupon");

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Tour",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tag",
                type: "nvarchar(40)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Node",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ValidationDate",
                table: "Coupon",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Tour");

            migrationBuilder.DropColumn(
                name: "ValidationDate",
                table: "Coupon");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tag",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Node",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Redemption",
                table: "Coupon",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Featured",
                columns: table => new
                {
                    TourId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Featured", x => x.TourId);
                    table.ForeignKey(
                        name: "FK_Featured_Tour_TourId",
                        column: x => x.TourId,
                        principalTable: "Tour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ImageId",
                table: "Tour",
                column: "ImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Image_ImageId",
                table: "Tour",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
