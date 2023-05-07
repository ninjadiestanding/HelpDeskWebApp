using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeHelpDeskWebApp.Migrations
{
    public partial class _16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "MessageModel",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfWriting",
                table: "MessageModel",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_MessageModel_ApplicationUserId",
                table: "MessageModel",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageModel_AspNetUsers_ApplicationUserId",
                table: "MessageModel",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageModel_AspNetUsers_ApplicationUserId",
                table: "MessageModel");

            migrationBuilder.DropIndex(
                name: "IX_MessageModel_ApplicationUserId",
                table: "MessageModel");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "MessageModel");

            migrationBuilder.DropColumn(
                name: "DateOfWriting",
                table: "MessageModel");
        }
    }
}
