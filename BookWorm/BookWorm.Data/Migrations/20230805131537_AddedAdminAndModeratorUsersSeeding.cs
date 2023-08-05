using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorm.Data.Migrations
{
    public partial class AddedAdminAndModeratorUsersSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("52331e18-a42a-4cb5-aaa9-6d6a2f8a462b"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("7aa69a0f-dce5-4c6a-bc66-713ad0439871"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("a5b768ad-a9a1-457e-b25b-1753855e3508"));

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("5a6d7ad3-578d-4d81-a011-5ef359e80dbd"), 0, "99567f57-06ea-41bf-9e53-6c165534fe5c", "moderator2@bookworm.com", false, true, null, "MODERATOR2@BOOKWORM.COM", "MODERATOR2@BOOKWORM.COM", "AQAAAAEAACcQAAAAEIvwkhYsdf6k7M0Sv27DYiRxk/kWHv0gNM3dKOEi92hpvt8/YPZZINufnQeDPzzOng==", null, false, "GBGKNZCL6MTHX3QHMAV4TISDHFRDBAF7", false, "moderator2@bookworm.com" },
                    { new Guid("7886266b-1282-484a-a4b2-1dd2644a94d1"), 0, "f13caeaf-6cbe-4608-8b5b-4434d2d282fd", "admin@bookworm.com", false, true, null, "ADMIN@BOOKWORM.COM", "ADMIN@BOOKWORM.COM", "AQAAAAEAACcQAAAAEFAdC3im6xmdFIsf+MsIcopBuSNFz2HU15zaS5SjNu1VMUdSHfYa/iF0NxxiqNA9uw==", null, false, "T4DMIR6DSDAR5R7QTFMX6MR6QSOXGGHH", false, "admin@bookworm.com" },
                    { new Guid("81b3b5ec-c0ed-4a48-9575-924e159dedf5"), 0, "cfb269b1-7833-439e-aa6e-f1f9a3fc484d", "moderator1@bookworm.com", false, true, null, "MODERATOR1@BOOKWORM.COM", "MODERATOR1@BOOKWORM.COM", "AQAAAAEAACcQAAAAEMajAP3maoVrRPWh5JTafBJE4KzjiKWZoF9LQd6/+xmPFuATvKwZbxIg0+i5mi15mg==", null, false, "AVBJCLXQD6IX5FDGTE37SHAZGVJWUJVT", false, "moderator1@bookworm.com" }
                });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[,]
                {
                    { new Guid("0ce97fb5-e694-45b3-9d0a-055adb374013"), new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 1, "From childhood’s hour I have not been\r\nAs others were—I have not seen\r\nAs others saw—I could not bring\r\nMy passions from a common spring—\r\nFrom the same source I have not taken\r\nMy sorrow—I could not awaken\r\nMy heart to joy at the same tone—\r\nAnd all I lov’d—I lov’d alone—\r\nThen—in my childhood—in the dawn\r\nOf a most stormy life—was drawn\r\nFrom ev’ry depth of good and ill\r\nThe mystery which binds me still—\r\nFrom the torrent, or the fountain—\r\nFrom the red cliff of the mountain—\r\nFrom the sun that ’round me roll’d\r\nIn its autumn tint of gold—\r\nFrom the lightning in the sky\r\nAs it pass’d me flying by—\r\nFrom the thunder, and the storm—\r\nAnd the cloud that took the form\r\n(When the rest of Heaven was blue)\r\nOf a demon in my view—", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Edgar Allan Poe's work!", false, false, "Alone" },
                    { new Guid("737e55ff-cb83-45ab-b54c-e9291c10f026"), new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 5, "Shall I compare thee to a summer’s day?\r\nThou art more lovely and more temperate:\r\nRough winds do shake the darling buds of May,\r\nAnd summer’s lease hath all too short a date;\r\nSometime too hot the eye of heaven shines,\r\nAnd often is his gold complexion dimm'd;\r\nAnd every fair from fair sometime declines,\r\nBy chance or nature’s changing course untrimm'd;\r\nBut thy eternal summer shall not fade,\r\nNor lose possession of that fair thou ow’st;\r\nNor shall death brag thou wander’st in his shade,\r\nWhen in eternal lines to time thou grow’st:\r\n   So long as men can breathe or eyes can see,\r\n   So long lives this, and this gives life to thee.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is William Shakespeare's work!", false, false, "Sonnet 18: Shall I compare thee to a summer’s day?" },
                    { new Guid("d93bce43-4fc4-4f98-b0c8-3c06b8844e14"), new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 6, "“Hope” is the thing with feathers -\r\nThat perches in the soul -\r\nAnd sings the tune without the words -\r\nAnd never stops - at all -\r\n\r\nAnd sweetest - in the Gale - is heard -\r\nAnd sore must be the storm -\r\nThat could abash the little Bird\r\nThat kept so many warm -\r\n\r\nI’ve heard it in the chillest land -\r\nAnd on the strangest Sea -\r\nYet - never - in Extremity,\r\nIt asked a crumb - of me.\r\n", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Emily Dickinson's work!", false, false, "“Hope” is the thing with feathers" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("5a6d7ad3-578d-4d81-a011-5ef359e80dbd"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7886266b-1282-484a-a4b2-1dd2644a94d1"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("81b3b5ec-c0ed-4a48-9575-924e159dedf5"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("0ce97fb5-e694-45b3-9d0a-055adb374013"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("737e55ff-cb83-45ab-b54c-e9291c10f026"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("d93bce43-4fc4-4f98-b0c8-3c06b8844e14"));

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("52331e18-a42a-4cb5-aaa9-6d6a2f8a462b"), new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 5, "Shall I compare thee to a summer’s day?\r\nThou art more lovely and more temperate:\r\nRough winds do shake the darling buds of May,\r\nAnd summer’s lease hath all too short a date;\r\nSometime too hot the eye of heaven shines,\r\nAnd often is his gold complexion dimm'd;\r\nAnd every fair from fair sometime declines,\r\nBy chance or nature’s changing course untrimm'd;\r\nBut thy eternal summer shall not fade,\r\nNor lose possession of that fair thou ow’st;\r\nNor shall death brag thou wander’st in his shade,\r\nWhen in eternal lines to time thou grow’st:\r\n   So long as men can breathe or eyes can see,\r\n   So long lives this, and this gives life to thee.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is William Shakespeare's work!", false, false, "Sonnet 18: Shall I compare thee to a summer’s day?" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("7aa69a0f-dce5-4c6a-bc66-713ad0439871"), new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 1, "From childhood’s hour I have not been\r\nAs others were—I have not seen\r\nAs others saw—I could not bring\r\nMy passions from a common spring—\r\nFrom the same source I have not taken\r\nMy sorrow—I could not awaken\r\nMy heart to joy at the same tone—\r\nAnd all I lov’d—I lov’d alone—\r\nThen—in my childhood—in the dawn\r\nOf a most stormy life—was drawn\r\nFrom ev’ry depth of good and ill\r\nThe mystery which binds me still—\r\nFrom the torrent, or the fountain—\r\nFrom the red cliff of the mountain—\r\nFrom the sun that ’round me roll’d\r\nIn its autumn tint of gold—\r\nFrom the lightning in the sky\r\nAs it pass’d me flying by—\r\nFrom the thunder, and the storm—\r\nAnd the cloud that took the form\r\n(When the rest of Heaven was blue)\r\nOf a demon in my view—", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Edgar Allan Poe's work!", false, false, "Alone" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("a5b768ad-a9a1-457e-b25b-1753855e3508"), new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 6, "“Hope” is the thing with feathers -\r\nThat perches in the soul -\r\nAnd sings the tune without the words -\r\nAnd never stops - at all -\r\n\r\nAnd sweetest - in the Gale - is heard -\r\nAnd sore must be the storm -\r\nThat could abash the little Bird\r\nThat kept so many warm -\r\n\r\nI’ve heard it in the chillest land -\r\nAnd on the strangest Sea -\r\nYet - never - in Extremity,\r\nIt asked a crumb - of me.\r\n", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Emily Dickinson's work!", false, false, "“Hope” is the thing with feathers" });
        }
    }
}
