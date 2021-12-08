using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class NewDbConfigurations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "StudentAttendances",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "StudentAttendances",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Grade",
                table: "StudentAttendances",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "AttendanceShedulers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AttendanceShedulers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "Attendances",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Attended",
                table: "Attendances",
                type: "character varying(1)",
                maxLength: 1,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "StudentAttendances",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "StudentAttendances",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Grade",
                table: "StudentAttendances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "SubjectName",
                table: "AttendanceShedulers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AttendanceShedulers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "StudentName",
                table: "Attendances",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "Attended",
                table: "Attendances",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1)",
                oldMaxLength: 1);
        }
    }
}
