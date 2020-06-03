using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class add_Creator_Name : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorFirstName",
                table: "Project",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorLastName",
                table: "Project",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorFirstName",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatorLastName",
                table: "Project");
        }
    }
}
