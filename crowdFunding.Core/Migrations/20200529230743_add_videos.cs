using Microsoft.EntityFrameworkCore.Migrations;

namespace crowdFunding.Core.Migrations
{
    public partial class add_videos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackedProjects_User_UserId",
                table: "BackedProjects");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BackedProjects",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Photo",
                columns: table => new
                {
                    PhotoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photo", x => x.PhotoId);
                    table.ForeignKey(
                        name: "FK_Photo_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    PostsId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Post = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.PostsId);
                    table.ForeignKey(
                        name: "FK_Posts_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Video",
                columns: table => new
                {
                    VideoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Video", x => x.VideoId);
                    table.ForeignKey(
                        name: "FK_Video_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Photo_ProjectId",
                table: "Photo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_ProjectId",
                table: "Posts",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Video_ProjectId",
                table: "Video",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_BackedProjects_User_UserId",
                table: "BackedProjects",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackedProjects_User_UserId",
                table: "BackedProjects");

            migrationBuilder.DropTable(
                name: "Photo");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Video");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BackedProjects",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BackedProjects_User_UserId",
                table: "BackedProjects",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
