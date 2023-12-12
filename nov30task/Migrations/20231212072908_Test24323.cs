using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nov30task.Migrations
{
    public partial class Test24323 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avability",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookCode",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "CoverImageUrl",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ExTax",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "RewardPoints",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "IsLeft",
                table: "Sliders",
                newName: "Position");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Sliders",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64);

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogTag",
                columns: table => new
                {
                    BlogIdId = table.Column<int>(type: "int", nullable: false),
                    TagIdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogTag", x => new { x.BlogIdId, x.TagIdId });
                    table.ForeignKey(
                        name: "FK_BlogTag_Blogs_BlogIdId",
                        column: x => x.BlogIdId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogTag_Tags_TagIdId",
                        column: x => x.TagIdId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagIdId",
                table: "BlogTag",
                column: "TagIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.RenameColumn(
                name: "Position",
                table: "Sliders",
                newName: "IsLeft");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Sliders",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avability",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BookCode",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CoverImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "ExTax",
                table: "Books",
                type: "smallmoney",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "RewardPoints",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
