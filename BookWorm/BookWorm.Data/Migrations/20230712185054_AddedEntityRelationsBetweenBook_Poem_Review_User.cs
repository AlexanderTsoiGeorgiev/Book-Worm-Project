using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorm.Data.Migrations
{
    public partial class AddedEntityRelationsBetweenBook_Poem_Review_User : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "PoemId",
                table: "Reviews",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Poems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "BookPoem",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false),
                    PoemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPoem", x => new { x.PoemId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookPoem_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookPoem_Poems_PoemId",
                        column: x => x.PoemId,
                        principalTable: "Poems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ApplicationUserId",
                table: "Reviews",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_PoemId",
                table: "Reviews",
                column: "PoemId");

            migrationBuilder.CreateIndex(
                name: "IX_Poems_ApplicationUserId",
                table: "Poems",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPoem_BookId",
                table: "BookPoem",
                column: "BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_AspNetUsers_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_AspNetUsers_ApplicationUserId",
                table: "Poems",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                table: "Reviews",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Poems_PoemId",
                table: "Reviews",
                column: "PoemId",
                principalTable: "Poems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_AspNetUsers_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Poems_AspNetUsers_ApplicationUserId",
                table: "Poems");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Poems_PoemId",
                table: "Reviews");

            migrationBuilder.DropTable(
                name: "BookPoem");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ApplicationUserId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_BookId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_PoemId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Poems_ApplicationUserId",
                table: "Poems");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "BookId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "PoemId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Poems");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");
        }
    }
}
