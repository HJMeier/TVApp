using Microsoft.EntityFrameworkCore.Migrations;

namespace TVApp.Migrations.Results
{
    public partial class _1ChangeDisciplineToDisciplineId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discipline",
                table: "Result");

            migrationBuilder.AddColumn<int>(
                name: "DisciplineId",
                table: "Result",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisciplineId",
                table: "Result");

            migrationBuilder.AddColumn<string>(
                name: "Discipline",
                table: "Result",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
