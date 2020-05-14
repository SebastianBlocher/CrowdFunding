using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Migrations
{
    public partial class todayMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Backer",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Backer");
        }
    }
}
