using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LJH.VRTool.Migrations
{
    public partial class _201908184 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentTeacherId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "StudentTeachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentTeachers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Age = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    StudentTeacherId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_StudentTeachers_StudentTeacherId",
                        column: x => x.StudentTeacherId,
                        principalTable: "StudentTeachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentTeacherId",
                table: "Students",
                column: "StudentTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StudentTeacherId",
                table: "Teachers",
                column: "StudentTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentTeachers_StudentTeacherId",
                table: "Students",
                column: "StudentTeacherId",
                principalTable: "StudentTeachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentTeachers_StudentTeacherId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "StudentTeachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentTeacherId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentTeacherId",
                table: "Students");
        }
    }
}
