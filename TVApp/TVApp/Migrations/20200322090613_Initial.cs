using Microsoft.EntityFrameworkCore.Migrations;

namespace TVApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participant",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PreName = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    YearOfBirth = table.Column<int>(nullable: false),
                    Club = table.Column<string>(nullable: false),
                    Cathegory = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participant", x => x.ParticipantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participant");
        }
    }
}
