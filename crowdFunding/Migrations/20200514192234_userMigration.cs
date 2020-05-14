using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Migrations
{
    public partial class userMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Project_ProjectCreator_ProjectCreatorId",
                table: "Project");

            migrationBuilder.DropTable(
                name: "BackerRewardPackage");

            migrationBuilder.DropTable(
                name: "ProjectBacker");

            migrationBuilder.DropTable(
                name: "ProjectCreator");

            migrationBuilder.DropTable(
                name: "Backer");

            migrationBuilder.DropIndex(
                name: "IX_Project_ProjectCreatorId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "ProjectCreatorId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Project",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "BackedProjects",
                columns: table => new
                {
                    BackedProjectsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectId = table.Column<int>(nullable: false),
                    BackedOn = table.Column<DateTimeOffset>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackedProjects", x => x.BackedProjectsId);
                    table.ForeignKey(
                        name: "FK_BackedProjects_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_UserId",
                table: "Project",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BackedProjects_UserId",
                table: "BackedProjects",
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

            migrationBuilder.DropTable(
                name: "BackedProjects");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Project_UserId",
                table: "Project");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Project");

            migrationBuilder.AddColumn<int>(
                name: "ProjectCreatorId",
                table: "Project",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Backer",
                columns: table => new
                {
                    BackerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backer", x => x.BackerId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCreator",
                columns: table => new
                {
                    ProjectCreatorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCreator", x => x.ProjectCreatorId);
                });

            migrationBuilder.CreateTable(
                name: "BackerRewardPackage",
                columns: table => new
                {
                    BackerId = table.Column<int>(type: "int", nullable: false),
                    RewardPackageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BackerRewardPackage", x => new { x.BackerId, x.RewardPackageId });
                    table.ForeignKey(
                        name: "FK_BackerRewardPackage_Backer_BackerId",
                        column: x => x.BackerId,
                        principalTable: "Backer",
                        principalColumn: "BackerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BackerRewardPackage_RewardPackage_RewardPackageId",
                        column: x => x.RewardPackageId,
                        principalTable: "RewardPackage",
                        principalColumn: "RewardPackageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBacker",
                columns: table => new
                {
                    BackerId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBacker", x => new { x.BackerId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectBacker_Backer_BackerId",
                        column: x => x.BackerId,
                        principalTable: "Backer",
                        principalColumn: "BackerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectBacker_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ProjectCreatorId",
                table: "Project",
                column: "ProjectCreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_BackerRewardPackage_RewardPackageId",
                table: "BackerRewardPackage",
                column: "RewardPackageId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBacker_ProjectId",
                table: "ProjectBacker",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Project_ProjectCreator_ProjectCreatorId",
                table: "Project",
                column: "ProjectCreatorId",
                principalTable: "ProjectCreator",
                principalColumn: "ProjectCreatorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
