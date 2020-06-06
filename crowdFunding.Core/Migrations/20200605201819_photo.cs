using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackedProjects_Photo_PhotoId",
                table: "BackedProjects");

            migrationBuilder.DropIndex(
                name: "IX_BackedProjects_PhotoId",
                table: "BackedProjects");

            migrationBuilder.DropColumn(
                name: "PhotoId",
                table: "BackedProjects");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "BackedProjects",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "BackedProjects");

            migrationBuilder.AddColumn<int>(
                name: "PhotoId",
                table: "BackedProjects",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BackedProjects_PhotoId",
                table: "BackedProjects",
                column: "PhotoId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackedProjects_Photo_PhotoId",
                table: "BackedProjects",
                column: "PhotoId",
                principalTable: "Photo",
                principalColumn: "PhotoId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
