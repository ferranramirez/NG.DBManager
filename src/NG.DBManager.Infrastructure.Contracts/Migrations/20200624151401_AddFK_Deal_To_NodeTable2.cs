using Microsoft.EntityFrameworkCore.Migrations;

namespace NG.DBManager.Infrastructure.Contracts.Migrations
{
    public partial class AddFK_Deal_To_NodeTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CommerceDeal_CommerceId",
                table: "CommerceDeal");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CommerceDeal_CommerceId",
                table: "CommerceDeal",
                column: "CommerceId",
                unique: true);
        }
    }
}
