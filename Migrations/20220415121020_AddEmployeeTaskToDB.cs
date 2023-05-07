using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class AddEmployeeTaskToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.AddColumn<int>(
                name: "Criticality",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExecutionStatus",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerformanceEvaluation",
                table: "EmployeeTask",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeTask_TaskType_TaskTypeId",
                table: "EmployeeTask");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeTask_TaskTypeId",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "Criticality",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "ExecutionStatus",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "PerformanceEvaluation",
                table: "EmployeeTask");

            migrationBuilder.DropColumn(
                name: "TaskTypeId",
                table: "EmployeeTask");

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });
        }
    }
}
