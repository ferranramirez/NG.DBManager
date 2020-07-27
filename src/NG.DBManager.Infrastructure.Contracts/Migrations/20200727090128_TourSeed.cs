using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class TourSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tour",
                columns: new[] { "Id", "Created", "Description", "Duration", "GeoJson", "ImageId", "IsFeatured", "IsPremium", "Name" },
                values: new object[] { new Guid("e6cfc804-a418-4683-81be-ca9c753b698a"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, null, null, false, false, "Test Tour" });

            migrationBuilder.InsertData(
                table: "Node",
                columns: new[] { "Id", "DealId", "Description", "LocationId", "Name", "Order", "TourId" },
                values: new object[] { new Guid("080942cb-aad4-4e94-9385-f53e6c72113c"), null, null, new Guid("0013a98e-32f6-494d-b055-c9fb4dafc3e8"), "Test Node", 1, new Guid("e6cfc804-a418-4683-81be-ca9c753b698a") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Node",
                keyColumn: "Id",
                keyValue: new Guid("080942cb-aad4-4e94-9385-f53e6c72113c"));

            migrationBuilder.DeleteData(
                table: "Tour",
                keyColumn: "Id",
                keyValue: new Guid("e6cfc804-a418-4683-81be-ca9c753b698a"));
        }
    }
}
