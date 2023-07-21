using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorm.Data.Migrations
{
    public partial class AddedUserAndCategorySeeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 0, "a4276e99-7e24-486f-b71c-7ce187505e19", "edgar.allan.poe@bookworm.com", false, true, null, "EDGAR.ALLAN.POE@BOOKWORM.COM", "EDGAR.ALLAN.POE@BOOKWORM.COM", "AQAAAAEAACcQAAAAEDTAR8wcodXIl9pQ8QGZeDMNYS4otxanV+OPfZivLuOFrMwvUi32SrUHYAQuiPIVSw==", null, false, "MNGCJM2MITII262L7SZTKNPY7PRPJOH2", false, "edgar.allan.poe@bookworm.com" },
                    { new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 0, "bdc117b5-2aa2-46a1-b7f0-3d577cf0d11c", "william.shakespeare@bookworm.com", false, true, null, "WILLIAM.SHAKESPEARE@BOOKWORM.COM", "WILLIAM.SHAKESPEARE@BOOKWORM.COM", "AQAAAAEAACcQAAAAEBGdXj3rAQCeGfRHE3d4NrOwN1TSBHbOD803OIq3iPnMFTGt5+YhvTiwW+0EVzvHdQ==", null, false, "3BZWTPUWLSQAUOZXXWD6IXNCXDZM5R7O", false, "william.shakespeare@bookworm.com" },
                    { new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 0, "e7bf6169-29b1-474f-9506-370fdd6a91fd", "emily.dickinson@bookworm.com", false, true, null, "EMILY.DICKINSON@BOOKWORM.COM", "EMILY.DICKINSON@BOOKWORM.COM", "AQAAAAEAACcQAAAAEG/paiLvb+6WVLvIc7m9Vf+zmEqmDkm1x6MDLq9/Zlf6uQbI7uoEIRUwOTlJd1vj8w==", null, false, "QPUH2L4RXOISAQTVHN5GQAAIUTQA24BT", false, "emily.dickinson@bookworm.com" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Poem" },
                    { 2, "Lyric Poety" },
                    { 3, "Elegy" },
                    { 4, "Ode" },
                    { 5, "Sonnet" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
