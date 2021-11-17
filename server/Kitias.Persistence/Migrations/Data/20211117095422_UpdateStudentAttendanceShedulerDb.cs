using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateStudentAttendanceShedulerDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_AttendanceShedulers_AttendanceShedulerId",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_AttendanceShedulerId",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "AttendanceShedulerId",
                table: "StudentAttendances");

            migrationBuilder.AddColumn<Guid>(
                name: "ShedulerId",
                table: "StudentAttendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_ShedulerId",
                table: "StudentAttendances",
                column: "ShedulerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_AttendanceShedulers_ShedulerId",
                table: "StudentAttendances",
                column: "ShedulerId",
                principalTable: "AttendanceShedulers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_AttendanceShedulers_ShedulerId",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_ShedulerId",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "ShedulerId",
                table: "StudentAttendances");

            migrationBuilder.AddColumn<Guid>(
                name: "AttendanceShedulerId",
                table: "StudentAttendances",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_AttendanceShedulerId",
                table: "StudentAttendances",
                column: "AttendanceShedulerId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_AttendanceShedulers_AttendanceShedulerId",
                table: "StudentAttendances",
                column: "AttendanceShedulerId",
                principalTable: "AttendanceShedulers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
