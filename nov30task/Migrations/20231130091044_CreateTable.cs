using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nov30task.Migrations
{
    public partial class CreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ButtonText = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    IsLeft = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
