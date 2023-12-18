using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace nov30task.Migrations
{
    public partial class eff23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.DropIndex(
                name: "IX_BlogTag_BlogId",
                table: "BlogTag");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BlogTag");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Blogs");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "BlogTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "BlogId",
                table: "BlogTag",
                newName: "BlogsId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTag_TagId",
                table: "BlogTag",
                newName: "IX_BlogTag_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                columns: new[] { "BlogsId", "TagsId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag",
                column: "BlogsId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Blogs_BlogsId",
                table: "BlogTag");

            migrationBuilder.DropForeignKey(
                name: "FK_BlogTag_Tags_TagsId",
                table: "BlogTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag");

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "BlogTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "BlogsId",
                table: "BlogTag",
                newName: "BlogId");

            migrationBuilder.RenameIndex(
                name: "IX_BlogTag_TagsId",
                table: "BlogTag",
                newName: "IX_BlogTag_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BlogTag",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Blogs",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BlogTag",
                table: "BlogTag",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_BlogTag_BlogId",
                table: "BlogTag",
                column: "BlogId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Blogs_BlogId",
                table: "BlogTag",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BlogTag_Tags_TagId",
                table: "BlogTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
