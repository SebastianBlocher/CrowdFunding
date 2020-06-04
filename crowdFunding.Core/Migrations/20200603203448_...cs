using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class _ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Reward");

            migrationBuilder.DropColumn(
                name: "CreatorFirstName",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "CreatorLastName",
                table: "Project");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Reward",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorFirstName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorLastName",
                table: "Project",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
