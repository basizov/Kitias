using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateStudenAttendaceDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "StudentAttendances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_StudentAttendances_SubjectId",
                table: "StudentAttendances",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentAttendances_Subjects_SubjectId",
                table: "StudentAttendances",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentAttendances_Subjects_SubjectId",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "IX_StudentAttendances_SubjectId",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "StudentAttendances");
        }
    }
}
