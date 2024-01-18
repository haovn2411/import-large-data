using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Exercise1.Migrations
{
    public partial class updateDBtosave : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Score_Student_StudentId",
                table: "Score");

            migrationBuilder.DropForeignKey(
                name: "FK_Score_Subject_SubjectId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_StudentId",
                table: "Score");

            migrationBuilder.DropIndex(
                name: "IX_Score_SubjectId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "score",
                table: "Score");

            migrationBuilder.AddColumn<double>(
                name: "biology",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "chemistry",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "civic_education",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "english",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "geography",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "history",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "literature",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "mathematics",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "physics",
                table: "Score",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "province",
                table: "Score",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "student_id",
                table: "Score",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "biology",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "chemistry",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "civic_education",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "english",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "geography",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "history",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "literature",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "mathematics",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "physics",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "province",
                table: "Score");

            migrationBuilder.DropColumn(
                name: "student_id",
                table: "Score");

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Score",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SubjectId",
                table: "Score",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "score",
                table: "Score",
                type: "float",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Score_StudentId",
                table: "Score",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Score_SubjectId",
                table: "Score",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Student_StudentId",
                table: "Score",
                column: "StudentId",
                principalTable: "Student",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Score_Subject_SubjectId",
                table: "Score",
                column: "SubjectId",
                principalTable: "Subject",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
