using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class add_dates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedOn",
                table: "Project",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueTo",
                table: "Project",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueTo",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Posts");

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "CreatedOn",
                table: "Project",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
