using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Migrations
{
    public partial class rewardPackage_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ammount",
                table: "RewardPackage");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "RewardPackage",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "RewardPackage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "RewardPackage");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "RewardPackage");

            migrationBuilder.AddColumn<decimal>(
                name: "Ammount",
                table: "RewardPackage",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
