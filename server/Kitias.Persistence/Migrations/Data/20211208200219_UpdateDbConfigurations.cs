using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class UpdateDbConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Subjects_Date_Time",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "DateTimeIndex",
                table: "Subjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentAttendances_StudentName",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AttendanceShedulers_Name",
                table: "AttendanceShedulers");

            migrationBuilder.DropIndex(
                name: "ShedulerNameIndex",
                table: "AttendanceShedulers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Subjects_Date_Time_TeacherId",
                table: "Subjects",
                columns: new[] { "Date", "Time", "TeacherId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentAttendances_StudentName_SubjectName",
                table: "StudentAttendances",
                columns: new[] { "StudentName", "SubjectName" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AttendanceShedulers_Name_TeacherId",
                table: "AttendanceShedulers",
                columns: new[] { "Name", "TeacherId" });

            migrationBuilder.CreateIndex(
                name: "DateTimeIndex",
                table: "Subjects",
                columns: new[] { "Date", "Time", "TeacherId" },
                unique: true,
                filter: "\"Date\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances",
                columns: new[] { "StudentName", "SubjectName" },
                unique: true,
                filter: "\"StudentName\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ShedulerNameIndex",
                table: "AttendanceShedulers",
                columns: new[] { "Name", "TeacherId" },
                unique: true,
                filter: "\"Name\" IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Subjects_Date_Time_TeacherId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "DateTimeIndex",
                table: "Subjects");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_StudentAttendances_StudentName_SubjectName",
                table: "StudentAttendances");

            migrationBuilder.DropIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_AttendanceShedulers_Name_TeacherId",
                table: "AttendanceShedulers");

            migrationBuilder.DropIndex(
                name: "ShedulerNameIndex",
                table: "AttendanceShedulers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Subjects_Date_Time",
                table: "Subjects",
                columns: new[] { "Date", "Time" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_StudentAttendances_StudentName",
                table: "StudentAttendances",
                column: "StudentName");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AttendanceShedulers_Name",
                table: "AttendanceShedulers",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "DateTimeIndex",
                table: "Subjects",
                columns: new[] { "Date", "Time" },
                unique: true,
                filter: "\"Date\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "StudentNameIndex",
                table: "StudentAttendances",
                column: "StudentName",
                unique: true,
                filter: "\"StudentName\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "ShedulerNameIndex",
                table: "AttendanceShedulers",
                column: "Name",
                unique: true,
                filter: "\"Name\" IS NOT NULL");
        }
    }
}
