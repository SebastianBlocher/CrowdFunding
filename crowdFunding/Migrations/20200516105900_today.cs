using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Migrations
{
    public partial class today : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "BackedProjects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "BackedProjects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BackedProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "BackedProjects");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "BackedProjects");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BackedProjects");
        }
    }
}
