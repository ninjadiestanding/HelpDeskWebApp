using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "EmployeeTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_ApplicationUserId",
                table: "EmployeeTask",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_AspNetUsers_ApplicationUserId",
                table: "EmployeeTask",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_AspNetUsers_ApplicationUserId",
                table: "EmployeeTask");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTask_ApplicationUserId",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "EmployeeTask");
        }
    }
}
