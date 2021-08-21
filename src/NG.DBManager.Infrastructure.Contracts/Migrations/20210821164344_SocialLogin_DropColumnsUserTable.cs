using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class SocialLogin_DropColumnsUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO public.\"StandardUser\" SELECT \"Id\" AS \"UserId\", \"Password\", \"EmailConfirmed\" FROM public.\"User\";");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
            
            migrationBuilder.Sql("UPDATE public.\"User\" SET \"Password\" = su.\"Password\", \"EmailConfirmed\" = su.\"EmailConfirmed\"" +
                " FROM public.\"StandardUser\" su" +
                " WHERE \"Id\" = \"UserId\";");
        }
    }
}
