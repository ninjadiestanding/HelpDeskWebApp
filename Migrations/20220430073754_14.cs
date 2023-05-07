using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class _14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_EmployeeTask_EmployeeTaskId",
                table: "FileModel");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTaskId",
                table: "FileModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_EmployeeTask_EmployeeTaskId",
                table: "FileModel",
                column: "EmployeeTaskId",
                principalTable: "EmployeeTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_EmployeeTask_EmployeeTaskId",
                table: "FileModel");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeTaskId",
                table: "FileModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_EmployeeTask_EmployeeTaskId",
                table: "FileModel",
                column: "EmployeeTaskId",
                principalTable: "EmployeeTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
