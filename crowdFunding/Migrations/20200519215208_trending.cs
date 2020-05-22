using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Migrations
{
    public partial class trending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfBackers",
                table: "Project",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfBackers",
                table: "Project");
        }
    }
}
