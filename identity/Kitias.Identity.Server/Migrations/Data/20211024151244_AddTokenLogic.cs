using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kitias.Identity.Server.Migrations.Data
{
    public partial class AddTokenLogic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expires",
                table: "UserTokens",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expires",
                table: "UserTokens");
        }
    }
}
