using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Persistence.Migrations
{
    public partial class ChangeAttendanceAndSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGiveScore",
                table: "Attendances");

            migrationBuilder.AddColumn<bool>(
                name: "IsGiveScore",
                table: "Subjects",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsGiveScore",
                table: "Subjects");

            migrationBuilder.AddColumn<bool>(
                name: "IsGiveScore",
                table: "Attendances",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }
    }
}
