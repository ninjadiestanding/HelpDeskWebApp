using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_TaskType_TaskTypeId",
                table: "EmployeeTask");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTask_TaskTypeId",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "TaskTypeId",
                table: "EmployeeTask");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TaskTypeId",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeTask_TaskTypeId",
                table: "EmployeeTask",
                column: "TaskTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeTask_TaskType_TaskTypeId",
                table: "EmployeeTask",
                column: "TaskTypeId",
                principalTable: "TaskType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
