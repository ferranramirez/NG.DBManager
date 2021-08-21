using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class SocialLogin_CreateStandardAndSocialUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Provider = table.Column<int>(nullable: false),
                    SocialId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialUser", x => new { x.UserId, x.Provider });
                    table.ForeignKey(
                        name: "FK_SocialUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StandardUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StandardUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_StandardUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SocialUser_SocialId",
                table: "SocialUser",
                column: "SocialId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SocialUser");

            migrationBuilder.DropTable(
                name: "StandardUser");
        }
    }
}
