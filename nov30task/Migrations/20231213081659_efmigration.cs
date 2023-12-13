using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nov30task.Migrations
{
    public partial class efmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogIdId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagIdId",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_TagIdId",
                table: "BlogTag");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tags",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "TagIdId",
                table: "BlogTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "BlogIdId",
                table: "BlogTag",
                newName: "BlogId");

            migrationBuilder.AddColumn<int>(
                name: "BlogId",
                table: "Tags",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BlogTag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_BlogId",
                table: "Tags",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagId",
                table: "BlogTag",
                column: "TagId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_Blogs_BlogId",
                table: "Tags",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_Blogs_BlogId",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_BlogId",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_TagId",
                table: "BlogTag");

            migrationBuilder.DropColumn(
                name: "BlogId",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlogTag");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Tags",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "BlogTag",
                newName: "TagIdId");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "BlogTag",
                newName: "BlogIdId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                columns: new[] { "BlogIdId", "TagIdId" });

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_TagIdId",
                table: "BlogTag",
                column: "TagIdId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogIdId",
                table: "BlogTag",
                column: "BlogIdId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagIdId",
                table: "BlogTag",
                column: "TagIdId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
