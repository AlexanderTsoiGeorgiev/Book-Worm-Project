using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorm.Data.Migrations
{
    public partial class AddedFirstAndLastNameToApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("4653e0a6-579d-4d28-a762-197bcc80d917"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("8e41d45f-a630-4390-a458-12f905c35797"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("fad5b312-1126-4281-a2bd-774157ce1f8a"));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "Test");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "Test");

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("12068328-79aa-472a-9c03-4cf585cc7cf9"), new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 6, "“Hope” is the thing with feathers -\r\nThat perches in the soul -\r\nAnd sings the tune without the words -\r\nAnd never stops - at all -\r\n\r\nAnd sweetest - in the Gale - is heard -\r\nAnd sore must be the storm -\r\nThat could abash the little Bird\r\nThat kept so many warm -\r\n\r\nI’ve heard it in the chillest land -\r\nAnd on the strangest Sea -\r\nYet - never - in Extremity,\r\nIt asked a crumb - of me.\r\n", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Emily Dickinson's work!", false, false, "“Hope” is the thing with feathers" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("2eb39899-eeb5-4d00-bcc7-28af8b538969"), new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 5, "Shall I compare thee to a summer’s day?\r\nThou art more lovely and more temperate:\r\nRough winds do shake the darling buds of May,\r\nAnd summer’s lease hath all too short a date;\r\nSometime too hot the eye of heaven shines,\r\nAnd often is his gold complexion dimm'd;\r\nAnd every fair from fair sometime declines,\r\nBy chance or nature’s changing course untrimm'd;\r\nBut thy eternal summer shall not fade,\r\nNor lose possession of that fair thou ow’st;\r\nNor shall death brag thou wander’st in his shade,\r\nWhen in eternal lines to time thou grow’st:\r\n   So long as men can breathe or eyes can see,\r\n   So long lives this, and this gives life to thee.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is William Shakespeare's work!", false, false, "Sonnet 18: Shall I compare thee to a summer’s day?" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("b95c5f98-81bc-45c1-bc19-2d8869defa7d"), new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 1, "From childhood’s hour I have not been\r\nAs others were—I have not seen\r\nAs others saw—I could not bring\r\nMy passions from a common spring—\r\nFrom the same source I have not taken\r\nMy sorrow—I could not awaken\r\nMy heart to joy at the same tone—\r\nAnd all I lov’d—I lov’d alone—\r\nThen—in my childhood—in the dawn\r\nOf a most stormy life—was drawn\r\nFrom ev’ry depth of good and ill\r\nThe mystery which binds me still—\r\nFrom the torrent, or the fountain—\r\nFrom the red cliff of the mountain—\r\nFrom the sun that ’round me roll’d\r\nIn its autumn tint of gold—\r\nFrom the lightning in the sky\r\nAs it pass’d me flying by—\r\nFrom the thunder, and the storm—\r\nAnd the cloud that took the form\r\n(When the rest of Heaven was blue)\r\nOf a demon in my view—", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Edgar Allan Poe's work!", false, false, "Alone" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("12068328-79aa-472a-9c03-4cf585cc7cf9"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("2eb39899-eeb5-4d00-bcc7-28af8b538969"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("b95c5f98-81bc-45c1-bc19-2d8869defa7d"));

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("4653e0a6-579d-4d28-a762-197bcc80d917"), new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 6, "“Hope” is the thing with feathers -\r\nThat perches in the soul -\r\nAnd sings the tune without the words -\r\nAnd never stops - at all -\r\n\r\nAnd sweetest - in the Gale - is heard -\r\nAnd sore must be the storm -\r\nThat could abash the little Bird\r\nThat kept so many warm -\r\n\r\nI’ve heard it in the chillest land -\r\nAnd on the strangest Sea -\r\nYet - never - in Extremity,\r\nIt asked a crumb - of me.\r\n", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Emily Dickinson's work!", false, false, "“Hope” is the thing with feathers" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("8e41d45f-a630-4390-a458-12f905c35797"), new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 5, "Shall I compare thee to a summer’s day?\r\nThou art more lovely and more temperate:\r\nRough winds do shake the darling buds of May,\r\nAnd summer’s lease hath all too short a date;\r\nSometime too hot the eye of heaven shines,\r\nAnd often is his gold complexion dimm'd;\r\nAnd every fair from fair sometime declines,\r\nBy chance or nature’s changing course untrimm'd;\r\nBut thy eternal summer shall not fade,\r\nNor lose possession of that fair thou ow’st;\r\nNor shall death brag thou wander’st in his shade,\r\nWhen in eternal lines to time thou grow’st:\r\n   So long as men can breathe or eyes can see,\r\n   So long lives this, and this gives life to thee.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is William Shakespeare's work!", false, false, "Sonnet 18: Shall I compare thee to a summer’s day?" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("fad5b312-1126-4281-a2bd-774157ce1f8a"), new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 1, "From childhood’s hour I have not been\r\nAs others were—I have not seen\r\nAs others saw—I could not bring\r\nMy passions from a common spring—\r\nFrom the same source I have not taken\r\nMy sorrow—I could not awaken\r\nMy heart to joy at the same tone—\r\nAnd all I lov’d—I lov’d alone—\r\nThen—in my childhood—in the dawn\r\nOf a most stormy life—was drawn\r\nFrom ev’ry depth of good and ill\r\nThe mystery which binds me still—\r\nFrom the torrent, or the fountain—\r\nFrom the red cliff of the mountain—\r\nFrom the sun that ’round me roll’d\r\nIn its autumn tint of gold—\r\nFrom the lightning in the sky\r\nAs it pass’d me flying by—\r\nFrom the thunder, and the storm—\r\nAnd the cloud that took the form\r\n(When the rest of Heaven was blue)\r\nOf a demon in my view—", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Edgar Allan Poe's work!", false, false, "Alone" });
        }
    }
}
