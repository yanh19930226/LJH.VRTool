using Microsoft.EntityFrameworkCore.Migrations;

namespace LJH.VRTool.Migrations
{
    public partial class _201908185 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_StudentTeachers_StudentTeacherId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Teachers_StudentTeachers_StudentTeacherId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Teachers_StudentTeacherId",
                table: "Teachers");

            migrationBuilder.DropIndex(
                name: "IX_Students_StudentTeacherId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentTeacherId",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "StudentTeacherId",
                table: "Students");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentTeachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "StudentTeachers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeachers_StudentId",
                table: "StudentTeachers",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentTeachers_TeacherId",
                table: "StudentTeachers",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeachers_Students_StudentId",
                table: "StudentTeachers",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentTeachers_Teachers_TeacherId",
                table: "StudentTeachers",
                column: "TeacherId",
                principalTable: "Teachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeachers_Students_StudentId",
                table: "StudentTeachers");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentTeachers_Teachers_TeacherId",
                table: "StudentTeachers");

            migrationBuilder.DropIndex(
                name: "IX_StudentTeachers_StudentId",
                table: "StudentTeachers");

            migrationBuilder.DropIndex(
                name: "IX_StudentTeachers_TeacherId",
                table: "StudentTeachers");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentTeachers");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "StudentTeachers");

            migrationBuilder.AddColumn<int>(
                name: "StudentTeacherId",
                table: "Teachers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentTeacherId",
                table: "Students",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_StudentTeacherId",
                table: "Teachers",
                column: "StudentTeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentTeacherId",
                table: "Students",
                column: "StudentTeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_StudentTeachers_StudentTeacherId",
                table: "Students",
                column: "StudentTeacherId",
                principalTable: "StudentTeachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teachers_StudentTeachers_StudentTeacherId",
                table: "Teachers",
                column: "StudentTeacherId",
                principalTable: "StudentTeachers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
