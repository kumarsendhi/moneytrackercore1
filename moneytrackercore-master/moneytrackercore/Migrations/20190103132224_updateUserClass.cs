using Microsoft.EntityFrameworkCore.Migrations;

namespace moneytrackercore.Migrations
{
    public partial class updateUserClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BalanceId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BalanceId",
                table: "Users",
                column: "BalanceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Balance_BalanceId",
                table: "Users",
                column: "BalanceId",
                principalTable: "Balance",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Balance_BalanceId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BalanceId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BalanceId",
                table: "Users");
        }
    }
}
