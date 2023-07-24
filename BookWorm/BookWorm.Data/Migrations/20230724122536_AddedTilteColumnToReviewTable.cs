using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWorm.Data.Migrations
{
    public partial class AddedTilteColumnToReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("10fcc720-9e35-4eca-a462-704f0fc2e98f"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("ca095314-df4b-426b-9a24-de65f108d809"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("f05582b8-3bcb-4d94-a824-3f26bcf3e8d0"));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Reviews",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("0f7716e7-88da-416a-8dd3-5907e3861718"), new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 5, "Shall I compare thee to a summer’s day?\r\nThou art more lovely and more temperate:\r\nRough winds do shake the darling buds of May,\r\nAnd summer’s lease hath all too short a date;\r\nSometime too hot the eye of heaven shines,\r\nAnd often is his gold complexion dimm'd;\r\nAnd every fair from fair sometime declines,\r\nBy chance or nature’s changing course untrimm'd;\r\nBut thy eternal summer shall not fade,\r\nNor lose possession of that fair thou ow’st;\r\nNor shall death brag thou wander’st in his shade,\r\nWhen in eternal lines to time thou grow’st:\r\n   So long as men can breathe or eyes can see,\r\n   So long lives this, and this gives life to thee.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is William Shakespeare's work!", false, false, "Sonnet 18: Shall I compare thee to a summer’s day?" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("9232a681-c05d-467c-880f-229374b3b318"), new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 6, "“Hope” is the thing with feathers -\r\nThat perches in the soul -\r\nAnd sings the tune without the words -\r\nAnd never stops - at all -\r\n\r\nAnd sweetest - in the Gale - is heard -\r\nAnd sore must be the storm -\r\nThat could abash the little Bird\r\nThat kept so many warm -\r\n\r\nI’ve heard it in the chillest land -\r\nAnd on the strangest Sea -\r\nYet - never - in Extremity,\r\nIt asked a crumb - of me.\r\n", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Emily Dickinson's work!", false, false, "“Hope” is the thing with feathers" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("c9f934a3-30dd-485b-abeb-9ce3c8566b0c"), new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 1, "From childhood’s hour I have not been\r\nAs others were—I have not seen\r\nAs others saw—I could not bring\r\nMy passions from a common spring—\r\nFrom the same source I have not taken\r\nMy sorrow—I could not awaken\r\nMy heart to joy at the same tone—\r\nAnd all I lov’d—I lov’d alone—\r\nThen—in my childhood—in the dawn\r\nOf a most stormy life—was drawn\r\nFrom ev’ry depth of good and ill\r\nThe mystery which binds me still—\r\nFrom the torrent, or the fountain—\r\nFrom the red cliff of the mountain—\r\nFrom the sun that ’round me roll’d\r\nIn its autumn tint of gold—\r\nFrom the lightning in the sky\r\nAs it pass’d me flying by—\r\nFrom the thunder, and the storm—\r\nAnd the cloud that took the form\r\n(When the rest of Heaven was blue)\r\nOf a demon in my view—", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Edgar Allan Poe's work!", false, false, "Alone" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("0f7716e7-88da-416a-8dd3-5907e3861718"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("9232a681-c05d-467c-880f-229374b3b318"));

            migrationBuilder.DeleteData(
                table: "Poems",
                keyColumn: "Id",
                keyValue: new Guid("c9f934a3-30dd-485b-abeb-9ce3c8566b0c"));

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Reviews");

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("10fcc720-9e35-4eca-a462-704f0fc2e98f"), new Guid("d470da45-fd3c-4a54-accd-6088db795dfa"), 5, "Shall I compare thee to a summer’s day?\r\nThou art more lovely and more temperate:\r\nRough winds do shake the darling buds of May,\r\nAnd summer’s lease hath all too short a date;\r\nSometime too hot the eye of heaven shines,\r\nAnd often is his gold complexion dimm'd;\r\nAnd every fair from fair sometime declines,\r\nBy chance or nature’s changing course untrimm'd;\r\nBut thy eternal summer shall not fade,\r\nNor lose possession of that fair thou ow’st;\r\nNor shall death brag thou wander’st in his shade,\r\nWhen in eternal lines to time thou grow’st:\r\n   So long as men can breathe or eyes can see,\r\n   So long lives this, and this gives life to thee.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is William Shakespeare's work!", false, false, "Sonnet 18: Shall I compare thee to a summer’s day?" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("ca095314-df4b-426b-9a24-de65f108d809"), new Guid("a5ea65a0-7c43-4575-b825-24d2c12fe926"), 1, "From childhood’s hour I have not been\r\nAs others were—I have not seen\r\nAs others saw—I could not bring\r\nMy passions from a common spring—\r\nFrom the same source I have not taken\r\nMy sorrow—I could not awaken\r\nMy heart to joy at the same tone—\r\nAnd all I lov’d—I lov’d alone—\r\nThen—in my childhood—in the dawn\r\nOf a most stormy life—was drawn\r\nFrom ev’ry depth of good and ill\r\nThe mystery which binds me still—\r\nFrom the torrent, or the fountain—\r\nFrom the red cliff of the mountain—\r\nFrom the sun that ’round me roll’d\r\nIn its autumn tint of gold—\r\nFrom the lightning in the sky\r\nAs it pass’d me flying by—\r\nFrom the thunder, and the storm—\r\nAnd the cloud that took the form\r\n(When the rest of Heaven was blue)\r\nOf a demon in my view—", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Edgar Allan Poe's work!", false, false, "Alone" });

            migrationBuilder.InsertData(
                table: "Poems",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Content", "DateCreated", "DateEdited", "Description", "IsDeleted", "IsPrivate", "Title" },
                values: new object[] { new Guid("f05582b8-3bcb-4d94-a824-3f26bcf3e8d0"), new Guid("dd34d4c5-95fb-4132-a14c-27fd7c53d919"), 6, "“Hope” is the thing with feathers -\r\nThat perches in the soul -\r\nAnd sings the tune without the words -\r\nAnd never stops - at all -\r\n\r\nAnd sweetest - in the Gale - is heard -\r\nAnd sore must be the storm -\r\nThat could abash the little Bird\r\nThat kept so many warm -\r\n\r\nI’ve heard it in the chillest land -\r\nAnd on the strangest Sea -\r\nYet - never - in Extremity,\r\nIt asked a crumb - of me.\r\n", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "This is Emily Dickinson's work!", false, false, "“Hope” is the thing with feathers" });
        }
    }
}
