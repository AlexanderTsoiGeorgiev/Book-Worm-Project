using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorm.Data.Migrations
{
    public partial class AddedAuthorNavPropertyToPoemModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poems_AspNetUsers_ApplicationUserId",
                table: "Poems");

            migrationBuilder.DropIndex(
                name: "IX_Poems_ApplicationUserId",
                table: "Poems");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Poems");

            migrationBuilder.AddColumn<Guid>(
                name: "AuthorId",
                table: "Poems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Poems_AuthorId",
                table: "Poems",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_AspNetUsers_AuthorId",
                table: "Poems",
                column: "AuthorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Poems_AspNetUsers_AuthorId",
                table: "Poems");

            migrationBuilder.DropIndex(
                name: "IX_Poems_AuthorId",
                table: "Poems");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Poems");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "Poems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Poems_ApplicationUserId",
                table: "Poems",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Poems_AspNetUsers_ApplicationUserId",
                table: "Poems",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
