using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileModel_MessageModel_MessageModelId",
                table: "FileModel");

            migrationBuilder.DropIndex(
                name: "IX_FileModel_MessageModelId",
                table: "FileModel");

            migrationBuilder.DropColumn(
                name: "MessageModelId",
                table: "FileModel");

            migrationBuilder.AlterColumn<bool>(
                name: "Blocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MessageModelId",
                table: "FileModel",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Blocked",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FileModel_MessageModelId",
                table: "FileModel",
                column: "MessageModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileModel_MessageModel_MessageModelId",
                table: "FileModel",
                column: "MessageModelId",
                principalTable: "MessageModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
