using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sprint.Migrations
{
    public partial class CreateCartGameSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartGame",
                columns: table => new
                {
                    CartGameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    ReceivingUserId = table.Column<int>(nullable: false),
                    GameId = table.Column<int>(nullable: false),
                    AddedOn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartGame", x => x.CartGameId);
                    table.ForeignKey(
                        name: "FK_CartGame_CartUser",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartGame_Game",
                        column: x => x.GameId,
                        principalTable: "Game",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CartGame_ReceivingUser",
                        column: x => x.ReceivingUserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 1,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 2,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 3,
                column: "RegularPrice",
                value: 11.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 4,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 5,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 6,
                column: "RegularPrice",
                value: 10.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 7,
                column: "RegularPrice",
                value: 17.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 8,
                column: "RegularPrice",
                value: 43.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 9,
                column: "RegularPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 10,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 11,
                column: "RegularPrice",
                value: 14.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 12,
                column: "RegularPrice",
                value: 26.95m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 14,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 15,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 16,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 17,
                column: "RegularPrice",
                value: 35.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 18,
                column: "RegularPrice",
                value: 69.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 19,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 20,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 23,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 24,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 25,
                column: "RegularPrice",
                value: 22.79m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 26,
                column: "RegularPrice",
                value: 5.69m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 27,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 28,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 29,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 30,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "36bfd365-727c-4281-b0c7-86ac9e5030c5");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "ec5f42d7-549a-44ac-a2f3-d3fd6d21dde3");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "cb9ba883-fc9a-4a32-afb6-5f974dfe1e20", "15374399-42dc-4b5d-9e4d-4b3262b18191", "AQAAAAEAACcQAAAAELNPWcioO8ZhqyHVS2oi0rsDdnYMrS9mgz/FEik5eTl0xzNCGas6pooT41P/aUP7Rw==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "74e5c8b8-46a9-4b6f-9512-0ec81bcb5272", "bdb1b8d3-0afa-415c-9fc6-d19dca24a9b7", "AQAAAAEAACcQAAAAEFfZnXn4icWKa3Bv70XSkpG8WRIcMBbL1pneYq7ADnfuBRhFjxdzbAIvMkKWltHFSw==" });

            migrationBuilder.CreateIndex(
                name: "IX_CartGame_GameId",
                table: "CartGame",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_CartGame_ReceivingUserId",
                table: "CartGame",
                column: "ReceivingUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CartGame_UserId_ReceivingUserId_GameId",
                table: "CartGame",
                columns: new[] { "UserId", "ReceivingUserId", "GameId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartGame");

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 1,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 2,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 3,
                column: "RegularPrice",
                value: 11.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 4,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 5,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 6,
                column: "RegularPrice",
                value: 10.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 7,
                column: "RegularPrice",
                value: 17.49m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 8,
                column: "RegularPrice",
                value: 43.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 9,
                column: "RegularPrice",
                value: 26.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 10,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 11,
                column: "RegularPrice",
                value: 14.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 12,
                column: "RegularPrice",
                value: 26.95m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 14,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 15,
                column: "RegularPrice",
                value: 29.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 16,
                column: "RegularPrice",
                value: 21.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 17,
                column: "RegularPrice",
                value: 35.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 18,
                column: "RegularPrice",
                value: 69.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 19,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 20,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 23,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 24,
                column: "RegularPrice",
                value: 19.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 25,
                column: "RegularPrice",
                value: 22.79m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 26,
                column: "RegularPrice",
                value: 5.69m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 27,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 28,
                column: "RegularPrice",
                value: 79.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 29,
                column: "RegularPrice",
                value: 59.99m);

            migrationBuilder.UpdateData(
                table: "Game",
                keyColumn: "GameId",
                keyValue: 30,
                column: "RegularPrice",
                value: 39.99m);

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "b852f83a-ac5d-4c3b-89ef-70e60c6f864b");

            migrationBuilder.UpdateData(
                table: "Role",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "416ac185-ceef-4d72-9656-01f52fa2083c");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7d93dc65-40da-4b95-b0a6-8785be7efd20", "1f0b52f5-9fdc-4d96-9062-b31e2bd381d0", "AQAAAAEAACcQAAAAELD/iRjSZGy7qSRxUj5R0y08sqY+WiVMY473Ef502bgnGK+WYMBBb37WjZiUBONofw==" });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "AccountNum", "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f75d8b70-ce0f-42c5-9c05-3f996b263b34", "0b792e51-b6fe-43cc-9835-8e94468178f5", "AQAAAAEAACcQAAAAEEcfez/McSJqZmI/rmoYmwnHRuYcuDvDW+qTvCgAdnyAk2FFYqECz/OOT3JGbgFHdw==" });
        }
    }
}
