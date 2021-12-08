using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateStudentAttendaceDbConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentAttendances_StudentName_SubjectName",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentAttendances_StudentName_SubjectName_ShedulerId",
                table: "StudentAttendances",
                columns: new[] { "StudentName", "SubjectName", "ShedulerId" });

            migrationBuilder.CreateIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances",
                columns: new[] { "StudentName", "SubjectName", "ShedulerId" },
                unique: true,
                filter: "\"StudentName\" IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentAttendances_StudentName_SubjectName_ShedulerId",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentAttendances_StudentName_SubjectName",
                table: "StudentAttendances",
                columns: new[] { "StudentName", "SubjectName" });

            migrationBuilder.CreateIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances",
                columns: new[] { "StudentName", "SubjectName" },
                unique: true,
                filter: "\"StudentName\" IS NOT NULL");
        }
    }
}
