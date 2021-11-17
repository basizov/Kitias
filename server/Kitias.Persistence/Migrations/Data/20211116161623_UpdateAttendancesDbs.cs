using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateAttendancesDbs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_StudentAttendances_StudentAttendanceId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceShedulers_Groups_GroupId",
                table: "AttendanceShedulers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Teachers_TeacherId",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_TeacherId",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "StudentAttendances");

            migrationBuilder.RenameColumn(
                name: "StudentAttendanceId",
                table: "Attendances",
                newName: "AttendanceShedulerId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_StudentAttendanceId",
                table: "Attendances",
                newName: "IX_Attendances_AttendanceShedulerId");

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "AttendanceShedulers",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "AttendanceShedulers",
                type: "text",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_AttendanceShedulers_AttendanceShedulerId",
                table: "Attendances",
                column: "AttendanceShedulerId",
                principalTable: "AttendanceShedulers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceShedulers_Groups_GroupId",
                table: "AttendanceShedulers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendances_AttendanceShedulers_AttendanceShedulerId",
                table: "Attendances");

            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceShedulers_Groups_GroupId",
                table: "AttendanceShedulers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "AttendanceShedulers");

            migrationBuilder.RenameColumn(
                name: "AttendanceShedulerId",
                table: "Attendances",
                newName: "StudentAttendanceId");

            migrationBuilder.RenameIndex(
                name: "IX_Attendances_AttendanceShedulerId",
                table: "Attendances",
                newName: "IX_Attendances_StudentAttendanceId");

            migrationBuilder.AddColumn<Guid>(
                name: "TeacherId",
                table: "StudentAttendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<Guid>(
                name: "GroupId",
                table: "AttendanceShedulers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_TeacherId",
                table: "StudentAttendances",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendances_StudentAttendances_StudentAttendanceId",
                table: "Attendances",
                column: "StudentAttendanceId",
                principalTable: "StudentAttendances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceShedulers_Groups_GroupId",
                table: "AttendanceShedulers",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Teachers_TeacherId",
                table: "StudentAttendances",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
