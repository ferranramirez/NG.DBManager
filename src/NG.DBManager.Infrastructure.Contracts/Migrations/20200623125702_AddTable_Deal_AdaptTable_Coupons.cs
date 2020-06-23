using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class AddTable_Deal_AdaptTable_Coupons : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupon_Commerce_CommerceId",
                table: "Coupon");

            migrationBuilder.DropIndex(
                name: "IX_Coupon_CommerceId",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "CommerceId",
                table: "Coupon");

            migrationBuilder.AddColumn<Guid>(
                name: "NodeId",
                table: "Coupon",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Audio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Deal",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CommerceDeal",
                columns: table => new
                {
                    CommerceId = table.Column<Guid>(nullable: false),
                    DealId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommerceDeal", x => new { x.CommerceId, x.DealId });
                    table.ForeignKey(
                        name: "FK_CommerceDeal_Commerce_CommerceId",
                        column: x => x.CommerceId,
                        principalTable: "Commerce",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommerceDeal_Deal_DealId",
                        column: x => x.DealId,
                        principalTable: "Deal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tour_ImageId",
                table: "Tour",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_NodeId",
                table: "Coupon",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_CommerceDeal_CommerceId",
                table: "CommerceDeal",
                column: "CommerceId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommerceDeal_DealId",
                table: "CommerceDeal",
                column: "DealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupon_Node_NodeId",
                table: "Coupon",
                column: "NodeId",
                principalTable: "Node",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tour_Image_ImageId",
                table: "Tour",
                column: "ImageId",
                principalTable: "Image",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupon_Node_NodeId",
                table: "Coupon");

            migrationBuilder.DropForeignKey(
                name: "FK_Tour_Image_ImageId",
                table: "Tour");

            migrationBuilder.DropTable(
                name: "CommerceDeal");

            migrationBuilder.DropTable(
                name: "Deal");

            migrationBuilder.DropIndex(
                name: "IX_Tour_ImageId",
                table: "Tour");

            migrationBuilder.DropIndex(
                name: "IX_Coupon_NodeId",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Audio");

            migrationBuilder.AddColumn<Guid>(
                name: "CommerceId",
                table: "Coupon",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_CommerceId",
                table: "Coupon",
                column: "CommerceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupon_Commerce_CommerceId",
                table: "Coupon",
                column: "CommerceId",
                principalTable: "Commerce",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
