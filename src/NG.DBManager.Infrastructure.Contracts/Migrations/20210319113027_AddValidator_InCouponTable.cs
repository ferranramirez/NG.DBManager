using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class AddValidator_InCouponTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ValidatorId",
                table: "Coupon",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_ValidatorId",
                table: "Coupon",
                column: "ValidatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupon_User_ValidatorId",
                table: "Coupon",
                column: "ValidatorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupon_User_ValidatorId",
                table: "Coupon");

            migrationBuilder.DropIndex(
                name: "IX_Coupon_ValidatorId",
                table: "Coupon");

            migrationBuilder.DropColumn(
                name: "ValidatorId",
                table: "Coupon");
        }
    }
}
