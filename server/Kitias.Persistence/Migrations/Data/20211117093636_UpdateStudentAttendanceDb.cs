using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateStudentAttendanceDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Students_StudentId",
                table: "StudentAttendances");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "StudentAttendances",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "StudentAttendances",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Students_StudentId",
                table: "StudentAttendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Students_StudentId",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "StudentAttendances");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentId",
                table: "StudentAttendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Students_StudentId",
                table: "StudentAttendances",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
