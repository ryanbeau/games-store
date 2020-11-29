using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class CreatedEventsSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Wallet",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "Wallet",
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Wallet",
                maxLength: 16,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Address",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "Address",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Postal",
                table: "Address",
                maxLength: 6,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Address",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Event",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    EventName = table.Column<string>(nullable: false),
                    EventDescription = table.Column<string>(nullable: false),
                    EventDateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Event", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Event_Created_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventUser",
                columns: table => new
                {
                    EventUserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventUser", x => x.EventUserId);
                    table.ForeignKey(
                        name: "FK_Event_User",
                        column: x => x.EventId,
                        principalTable: "Event",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_User_Event",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "45df4e08-d79c-4dd5-b1c0-b2405bddc269");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "70a3dd0d-d5d9-4507-9391-2223a5caf080");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "bd6b7f90-b314-4b57-9520-feed8be1497c", "c9c74efc-584b-44ce-b8c2-80ea319f6be7", "AQAAAAEAACcQAAAAEG+UozRbllg/cdCaHScHwFV3kv2aGs4QeTTVbgP65vd/QTs/8zeMSCzxDklG7QbFBA==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7b02558a-91bc-4bc4-b072-1cbd8a1a971c", "de9d80c1-ddab-451e-8dee-b01807160deb", "AQAAAAEAACcQAAAAEJJIJ/jpRM/CP+E8r9h5150gmA3uWDBNGnjvLmruf8DtkTQCTiQSE8iNQhzeZDbpWA==" });

            migrationBuilder.CreateIndex(
                name: "IX_Event_UserId",
                table: "Event",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_EventId",
                table: "EventUser",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventUser_UserId",
                table: "EventUser",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventUser");

            migrationBuilder.DropTable(
                name: "Event");

            migrationBuilder.AlterColumn<string>(
                name: "Year",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "Month",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2);

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Wallet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 16);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Province",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Postal",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 6);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Address",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 30);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a2712888-33cb-4137-b089-894b7326ad6f");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "b04ac107-c2a0-4297-bbaf-f0976ce5e06e");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9f26980d-8098-46ab-843b-b2d8ff903979", "9dd7f504-86d8-4a4c-aa88-2535ad947084", "AQAAAAEAACcQAAAAELU+szr20ZJu3sIyE60CXxvFiRLd1davtXF8p+Ytc4zMP+C+lInWpszo75yJLizFsA==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "32af3121-7c5a-48f3-8816-a8db8973eacb", "ed37b2e4-340e-41f0-864d-03f2fd03b013", "AQAAAAEAACcQAAAAEBAKiTiLjYw0QXpwEX1AEzqIsI+4GqvahY02oO0KSAMD+VDdKHDh9L1QfSRYUHwoxA==" });
        }
    }
}
