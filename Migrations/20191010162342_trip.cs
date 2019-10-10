using Microsoft.EntityFrameworkCore.Migrations;

namespace TripPlanner.Migrations
{
    public partial class trip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "userprofiles",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "trips",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_userprofiles_ApplicationUserId",
                table: "userprofiles",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_userprofiles_AspNetUsers_ApplicationUserId",
                table: "userprofiles",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userprofiles_AspNetUsers_ApplicationUserId",
                table: "userprofiles");

            migrationBuilder.DropIndex(
                name: "IX_userprofiles_ApplicationUserId",
                table: "userprofiles");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "trips");

            migrationBuilder.AlterColumn<string>(
                name: "ApplicationUserId",
                table: "userprofiles",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
