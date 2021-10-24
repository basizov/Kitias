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

            migrationBuilder.AddColumn<string>(
                name: "FingerPrint",
                table: "UserTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "UserTokens",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "UserTokens",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expires",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "FingerPrint",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Token",
                table: "UserTokens");
        }
    }
}
