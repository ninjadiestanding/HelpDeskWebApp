using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class respuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ResponsibleUserId",
                table: "EmployeeTask",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_ResponsibleUserId",
                table: "EmployeeTask",
                column: "ResponsibleUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_AspNetUsers_ResponsibleUserId",
                table: "EmployeeTask",
                column: "ResponsibleUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_AspNetUsers_ResponsibleUserId",
                table: "EmployeeTask");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTask_ResponsibleUserId",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "ResponsibleUserId",
                table: "EmployeeTask");
        }
    }
}
