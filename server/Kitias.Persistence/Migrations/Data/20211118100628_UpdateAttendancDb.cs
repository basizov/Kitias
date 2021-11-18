using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateAttendancDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendanceShedulers_AttendanceShedulerId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_AttendanceShedulerId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "AttendanceShedulerId",
                table: "Attendances");

            migrationBuilder.AddColumn<Guid>(
                name: "ShedulerId",
                table: "Attendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_ShedulerId",
                table: "Attendances",
                column: "ShedulerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendanceShedulers_ShedulerId",
                table: "Attendances",
                column: "ShedulerId",
                principalTable: "AttendanceShedulers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendanceShedulers_ShedulerId",
                table: "Attendances");

            migrationBuilder.DropIndex(
                name: "IX_Attendances_ShedulerId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "ShedulerId",
                table: "Attendances");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceShedulerId",
                table: "Attendances",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AttendanceShedulerId",
                table: "Attendances",
                column: "AttendanceShedulerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendanceShedulers_AttendanceShedulerId",
                table: "Attendances",
                column: "AttendanceShedulerId",
                principalTable: "AttendanceShedulers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
