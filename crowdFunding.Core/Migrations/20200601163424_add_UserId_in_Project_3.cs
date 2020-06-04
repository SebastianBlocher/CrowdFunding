using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class add_UserId_in_Project_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_UserRefId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserRefId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserRefId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UserId",
                table: "Project",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_User_UserId",
                table: "Project");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "UserRefId",
                table: "Project",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserRefId",
                table: "Project",
                column: "UserRefId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_User_UserRefId",
                table: "Project",
                column: "UserRefId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
