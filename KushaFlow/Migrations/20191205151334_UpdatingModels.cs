using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KushaFlow.Migrations
{
    public partial class UpdatingModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentIsWork",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    WorkName = table.Column<string>(nullable: true),
                    WorkPath = table.Column<string>(nullable: true),
                    WorkFormat = table.Column<string>(nullable: true),
                    UploadDate = table.Column<DateTime>(nullable: false),
                    DownloadsNum = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentIsWork", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    ImgName = table.Column<string>(nullable: true),
                    ImgPath = table.Column<string>(nullable: true),
                    Institute = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Group = table.Column<string>(nullable: true),
                    Course = table.Column<string>(nullable: true),
                    Achievements = table.Column<string>(nullable: true),
                    InstagramAccount = table.Column<string>(nullable: true),
                    FacebookAccount = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkDownloads",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentIsWorkId = table.Column<int>(nullable: false),
                    DownloadDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDownloads", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDownloads_StudentIsWork_StudentIsWorkId",
                        column: x => x.StudentIsWorkId,
                        principalTable: "StudentIsWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkDownloads_StudentIsWorkId",
                table: "WorkDownloads",
                column: "StudentIsWorkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "WorkDownloads");

            migrationBuilder.DropTable(
                name: "StudentIsWork");
        }
    }
}
